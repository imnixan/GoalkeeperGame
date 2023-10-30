using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer
    : MonoBehaviour,
        ICatchListener,
        IPreparingListener,
        IKickHappenListener,
        IGoalListener
{
    [SerializeReference]
    private AudioClip ballKick,
        ballCatch,
        gatesGoal,
        overSound,
        ballCatchFans,
        prepareSound;
    private AudioSource audio;

    private void Awake()
    {
        audio = gameObject.AddComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetInt("SoundStatus", 1);
    }

    public void PlaySound(AudioClip sound)
    {
        audio.PlayOneShot(sound);
    }

    public void Vibrate()
    {
        if (PlayerPrefs.GetInt("VibroStatus", 1) == 1)
        {
            Handheld.Vibrate();
        }
    }

    public void OnKickHappen()
    {
        PlaySound(ballKick);
    }

    public void OnPrepare()
    {
        PlaySound(prepareSound);
    }

    public void OnCatchHappen()
    {
        PlaySound(ballCatch);
        PlaySound(ballCatchFans);
        Vibrate();
    }

    public void OnGoalHappen()
    {
        PlaySound(gatesGoal);
        PlaySound(overSound);
        Vibrate();
    }
}
