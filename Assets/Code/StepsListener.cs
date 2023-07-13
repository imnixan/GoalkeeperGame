using System.Collections;
using UnityEngine;

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

    private GameStepsMachine gameStepsMachine;

    public void ClickNext()
    {
        nextButton.SetActive(false);
        TipText.SetActive(true);
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

        ClickNext();
    }

    private void OnEnable()
    {
        Kicker.Kick += OnKickerKick;
        Ball.GoalHappend += OnGoalHappend;
        GoalKeeper.CatchBall += OnCatchBall;
        GoalKeeper.GoalKeeperReady += OnGoalKeeperReady;
    }

    private void OnDisable()
    {
        Kicker.Kick -= OnKickerKick;
        Ball.GoalHappend -= OnGoalHappend;
        GoalKeeper.CatchBall -= OnCatchBall;
        GoalKeeper.GoalKeeperReady -= OnGoalKeeperReady;
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
        gameStepsMachine.CurrentStep = GameStepsMachine.GameSteps.Preparing;

        Debug.Log("Catch listeners count " + catchListeners.Length);
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
}
