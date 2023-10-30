using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GamePauser : MonoBehaviour
{
    public static event UnityAction GamePausePressed;

    public static void PressPauseButton()
    {
        GamePausePressed?.Invoke();
    }
}
