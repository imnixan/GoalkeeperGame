using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatchCounter : MonoBehaviour, ICatchListener, IPreparingListener
{
    private const int Speed = 10;
    private const int ShowPosX = 0;
    private const int HidePosX = -350;
    private int catchCount;
    private TextMeshProUGUI catchesCounter;
    private RectTransform rt;

    private void Init()
    {
        catchesCounter = GetComponentInChildren<TextMeshProUGUI>();
        rt = GetComponent<RectTransform>();
    }

    public void OnCatchHappen()
    {
        catchCount++;
        catchesCounter.text = catchCount.ToString();
        StopAllCoroutines();
        StartCoroutine(ChangePosition(ShowPosX));
    }

    public void OnPrepare()
    {
        if (catchesCounter == null)
        {
            Init();
        }
        StopAllCoroutines();
        StartCoroutine(ChangePosition(HidePosX));
    }

    IEnumerator ChangePosition(int newPosX)
    {
        while (rt.anchoredPosition.x != newPosX)
        {
            rt.anchoredPosition = Vector2.MoveTowards(
                rt.anchoredPosition,
                new Vector2(newPosX, 0),
                Speed
            );
            yield return new WaitForFixedUpdate();
        }
    }
}
