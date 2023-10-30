using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ProjectSettings))]
public class GameMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CatchBall");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
}
