using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSprite : MonoBehaviour
{
    [SerializeField]
    private Sprite[] bgVars,
        gkVars,
        gatesVars;

    [SerializeField]
    private Image bg,
        gk,
        gates;

    private void Start()
    {
        bg.sprite = bgVars[PlayerPrefs.GetInt("BG")];
        gk.sprite = gkVars[PlayerPrefs.GetInt("GK")];
        gates.sprite = gatesVars[PlayerPrefs.GetInt("GATES")];
    }
}
