using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameStepsMachine : MonoBehaviour
{
    private GameSteps _currentStep;

    public GameSteps CurrentStep
    {
        get { return _currentStep; }
        set
        {
            if (_currentStep != value)
            {
                _currentStep = value;
            }
        }
    }

    public enum GameSteps
    {
        Null,
        WaitingNextButton,
        Preparing,
        Kick,
        Goal,
        Pause
    }
}
