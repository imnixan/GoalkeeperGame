using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Fan : MonoBehaviour, ICatchListener, IGoalListener
{
    public void OnCatchHappen()
    {
        image.sprite = succes;
        image.SetNativeSize();
        showFan.Restart();
    }

    [SerializeField]
    private Sprite succes,
        fail;
    private Image image;
    private RectTransform rt;
    private Sequence showFan;

    void Start()
    {
        image = GetComponent<Image>();
        rt = GetComponent<RectTransform>();

        showFan = DOTween
            .Sequence()
            .Append(rt.DOAnchorPosY(0, 0.3f))
            .AppendInterval(0.3f)
            .Append(rt.DOAnchorPosY(-1500, 0.3f));
    }

    public void OnGoalHappen()
    {
        image.sprite = fail;
        image.SetNativeSize();
        showFan.Restart();
    }
}
