using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

[RequireComponent(typeof(ProjectSettings))]
public class SettingsBehaviour : MonoBehaviour
{
    [SerializeReference]
    private Image vibroIcon,
        soundIcon;

    [SerializeReference]
    private Sprite[] vibroIcons,
        soundIcons;

    private const string ON = "ON";
    private const string OFF = "OFF";
    private const string VibroStatus = "VibroStatus";
    private const string SoundStatus = "SoundStatus";
    private const int TurnedON = 1;
    private const int TurnedOFF = 0;

    private void Start()
    {
        SetSettings();
    }

    private void SetSettings(int status, Image icon, Sprite[] sprites)
    {
        icon.sprite = sprites[status];
    }

    private void SetSettings()
    {
        SetSettings(PlayerPrefs.GetInt(SoundStatus, TurnedON), soundIcon, soundIcons);
        SetSettings(PlayerPrefs.GetInt(VibroStatus, TurnedON), vibroIcon, vibroIcons);
    }

    public void OnSoundButtonClick()
    {
        PlayerPrefs.SetInt(
            SoundStatus,
            PlayerPrefs.GetInt(SoundStatus, TurnedON) == TurnedON ? TurnedOFF : TurnedON
        );
        PlayerPrefs.Save();
        SetSettings(PlayerPrefs.GetInt(SoundStatus, TurnedON), soundIcon, soundIcons);
    }

    public void OnVibroButtonClick()
    {
        PlayerPrefs.SetInt(
            VibroStatus,
            PlayerPrefs.GetInt(VibroStatus, TurnedON) == TurnedON ? TurnedOFF : TurnedON
        );
        PlayerPrefs.Save();
        SetSettings(PlayerPrefs.GetInt(VibroStatus, TurnedON), vibroIcon, vibroIcons);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackMenu();
        }
    }
}
