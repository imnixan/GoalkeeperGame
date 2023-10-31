using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rules : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var seq = DOTween
                .Sequence()
                .Append(GetComponent<RectTransform>().DOAnchorPosY(1000, 0.3f))
                .AppendCallback(() =>
                {
                    gameObject.SetActive(false);
                });
            seq.Restart();
        }
    }
}
