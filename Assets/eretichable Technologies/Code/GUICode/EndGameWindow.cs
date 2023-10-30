using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndGameWindow : MonoBehaviour, IGoalListener, IPauseListener
{
    private const int Speed = 100;

    [SerializeReference]
    private TextMeshProUGUI finalScores,
        restartButtonText;

    private RectTransform rt;
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
        rt.DOAnchorPosX(1, 0.3f).Play();
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
            rt.DOAnchorPosX(1000, 0.3f).Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void OnPause()
    {
        pause = true;
        restartButtonText.text = "Continue";
        StopAllCoroutines();
        rt.DOAnchorPosX(1, 0.3f).Play();
    }

    public void Unpause()
    {
        pause = false;
        StopAllCoroutines();
        rt.DOAnchorPosX(1000, 0.3f).Play();
    }
}
