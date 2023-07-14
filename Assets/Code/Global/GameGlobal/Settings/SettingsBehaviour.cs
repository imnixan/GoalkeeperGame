using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    [SerializeReference]
    private TextMeshProUGUI vibroStatus,
        soundStatus;

    [SerializeField]
    private Color green,
        red;

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

    private void SetSettings(int status, Image icon, Sprite[] sprites, TextMeshProUGUI statusText)
    {
        icon.sprite = sprites[status];
        if (status == TurnedON)
        {
            statusText.text = ON;
            statusText.color = green;
            ;
        }
        else
        {
            statusText.text = OFF;
            statusText.color = red;
        }
    }

    private void SetSettings()
    {
        SetSettings(PlayerPrefs.GetInt(SoundStatus, TurnedON), soundIcon, soundIcons, soundStatus);
        SetSettings(PlayerPrefs.GetInt(VibroStatus, TurnedON), vibroIcon, vibroIcons, vibroStatus);
    }

    public void OnSoundButtonClick()
    {
        PlayerPrefs.SetInt(
            SoundStatus,
            PlayerPrefs.GetInt(SoundStatus, TurnedON) == TurnedON ? TurnedOFF : TurnedON
        );
        PlayerPrefs.Save();
        SetSettings(PlayerPrefs.GetInt(SoundStatus, TurnedON), soundIcon, soundIcons, soundStatus);
    }

    public void OnVibroButtonClick()
    {
        PlayerPrefs.SetInt(
            VibroStatus,
            PlayerPrefs.GetInt(VibroStatus, TurnedON) == TurnedON ? TurnedOFF : TurnedON
        );
        PlayerPrefs.Save();
        SetSettings(PlayerPrefs.GetInt(VibroStatus, TurnedON), vibroIcon, vibroIcons, vibroStatus);
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
