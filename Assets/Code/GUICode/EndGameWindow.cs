using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameWindow : MonoBehaviour, IGoalListener, IPauseListener
{
    private const int Speed = 100;

    [SerializeReference]
    private TextMeshProUGUI finalScores,
        restartButtonText;

    private RectTransform rt;
    private const int ShowPosY = 0;
    private const int HidePosY = 1200;
    private bool pause;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        finalScores = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnGoalHappen()
    {
        StopAllCoroutines();
        restartButtonText.text = "Play Again";
        StartCoroutine(ChangePosition(ShowPosY, false));
    }

    public void SetFinalScores(int scores)
    {
        finalScores.text = scores.ToString();
    }

    public void Restart()
    {
        StopAllCoroutines();
        if (pause)
        {
            GamePauser.PressPauseButton();
        }
        else
        {
            StartCoroutine(ChangePosition(HidePosY, true));
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator ChangePosition(int newPosY, bool restartAfter)
    {
        while (rt.anchoredPosition.y != newPosY)
        {
            rt.anchoredPosition = Vector2.MoveTowards(
                rt.anchoredPosition,
                new Vector2(0, newPosY),
                Speed
            );
            yield return new WaitForFixedUpdate();
        }
        if (restartAfter)
        {
            SceneManager.LoadScene("CatchBall");
        }
    }

    public void OnPause()
    {
        pause = true;
        restartButtonText.text = "Continue";
        StopAllCoroutines();
        StartCoroutine(ChangePosition(ShowPosY, false));
    }

    public void Unpause()
    {
        pause = false;
        StopAllCoroutines();
        StartCoroutine(ChangePosition(HidePosY, false));
    }
}
