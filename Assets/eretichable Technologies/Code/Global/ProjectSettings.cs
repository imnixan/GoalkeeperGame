using System.Collections;
using UnityEngine;

public class ProjectSettings : MonoBehaviour
{
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Application.targetFrameRate = 120;
    }
}
