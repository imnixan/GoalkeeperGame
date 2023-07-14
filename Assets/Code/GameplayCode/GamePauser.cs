using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GamePauser : MonoBehaviour
{
    public static event UnityAction GamePausePressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressPauseButton();
        }
    }

    public static void PressPauseButton()
    {
        GamePausePressed?.Invoke();
    }
}
