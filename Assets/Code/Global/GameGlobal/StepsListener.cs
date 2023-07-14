using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ProjectSettings))]
[RequireComponent(typeof(GamePauser))]
public class StepsListener : MonoBehaviour
{
    [SerializeReference]
    GameObject nextButton,
        TipText;

    private IKickHappenListener[] kickListeners;
    private IGoalListener[] goalListeners;
    private IPreparingListener[] prepareListeners;
    private IGoalKeeperReadyListener[] goalKeeperReadyListeners;
    private ICatchListener[] catchListeners;
    private IPauseListener[] pauseListeners;

    private GameStepsMachine gameStepsMachine;
    private GameStepsMachine.GameSteps stepBeforePause;

    private bool savedNextButtonStatus;

    public void ClickNext()
    {
        nextButton.SetActive(false);
        TipText.SetActive(true);
        gameStepsMachine.CurrentStep = GameStepsMachine.GameSteps.Preparing;
        foreach (var listener in prepareListeners)
        {
            listener.OnPrepare();
        }
    }

    private void Start()
    {
        gameStepsMachine = GetComponent<GameStepsMachine>();
        gameStepsMachine.CurrentStep = GameStepsMachine.GameSteps.Preparing;

        kickListeners = transform.GetComponentsInChildren<IKickHappenListener>();
        goalListeners = transform.GetComponentsInChildren<IGoalListener>();
        prepareListeners = transform.GetComponentsInChildren<IPreparingListener>();
        goalKeeperReadyListeners = transform.GetComponentsInChildren<IGoalKeeperReadyListener>();
        catchListeners = transform.GetComponentsInChildren<ICatchListener>();
        pauseListeners = transform.GetComponentsInChildren<IPauseListener>();

        ClickNext();
    }

    private void OnEnable()
    {
        Kicker.Kick += OnKickerKick;
        Ball.GoalHappend += OnGoalHappend;
        GoalKeeper.CatchBall += OnCatchBall;
        GoalKeeper.GoalKeeperReady += OnGoalKeeperReady;
        GamePauser.GamePausePressed += OnPausePressed;
    }

    private void OnDisable()
    {
        Kicker.Kick -= OnKickerKick;
        Ball.GoalHappend -= OnGoalHappend;
        GoalKeeper.CatchBall -= OnCatchBall;
        GoalKeeper.GoalKeeperReady -= OnGoalKeeperReady;
        GamePauser.GamePausePressed -= OnPausePressed;
    }

    private void OnKickerKick()
    {
        gameStepsMachine.CurrentStep = GameStepsMachine.GameSteps.Kick;
        foreach (var listener in kickListeners)
        {
            listener.OnKickHappen();
        }
    }

    private void OnGoalHappend()
    {
        gameStepsMachine.CurrentStep = GameStepsMachine.GameSteps.Goal;
        foreach (var listener in goalListeners)
        {
            listener.OnGoalHappen();
        }
    }

    private void OnCatchBall()
    {
        nextButton.SetActive(true);
        gameStepsMachine.CurrentStep = GameStepsMachine.GameSteps.WaitingNextButton;
        foreach (var listener in catchListeners)
        {
            listener.OnCatchHappen();
        }
    }

    private void OnGoalKeeperReady()
    {
        if (gameStepsMachine.CurrentStep == GameStepsMachine.GameSteps.Preparing)
        {
            TipText.SetActive(false);
            foreach (var listener in goalKeeperReadyListeners)
            {
                listener.OnGoalKeeperReady();
            }
        }
    }

    private void OnPausePressed()
    {
        if (gameStepsMachine.CurrentStep != GameStepsMachine.GameSteps.Pause)
        {
            stepBeforePause = gameStepsMachine.CurrentStep;
            gameStepsMachine.CurrentStep = GameStepsMachine.GameSteps.Pause;
            foreach (var listener in pauseListeners)
            {
                listener.OnPause();
            }
            savedNextButtonStatus = nextButton.activeSelf;
            nextButton.SetActive(false);
        }
        else
        {
            gameStepsMachine.CurrentStep = stepBeforePause;
            foreach (var listener in pauseListeners)
            {
                listener.Unpause();
            }
            nextButton.SetActive(savedNextButtonStatus);
        }
    }
}
