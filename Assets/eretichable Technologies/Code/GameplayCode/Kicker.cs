using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class Kicker : MonoBehaviour, IPreparingListener, IGoalKeeperReadyListener
{
    public static event UnityAction Kick;

    [SerializeReference]
    private GameObject kickEffect;

    [SerializeReference]
    private SplineCreator sc;

    [SerializeReference]
    private Ball ball;

    [SerializeReference]
    private Sprite kickLeftLeg,
        kickRightLeg,
        idle;

    [SerializeField]
    private GameObject helmet;

    [SerializeField]
    private RectTransform catchText;

    [SerializeField]
    private Sprite catchSprite,
        dodgeSprite;

    private Image catchImage;

    private Sprite kickSprite;
    private float kickForce = 13f;
    private float kickRotation = 3;
    private float screenHalfWidth;
    private Image image;
    private float ballRotationSpeed;

    private Sequence catchShow;

    private void Init()
    {
        image = GetComponent<Image>();
        screenHalfWidth = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x;
        sc.Init();
        ball.Init();
        catchImage = catchText.GetComponent<Image>();

        catchShow = DOTween
            .Sequence()
            .AppendCallback(() =>
            {
                catchText.anchoredPosition = new Vector2(-1000, 0);
                catchText.eulerAngles = new Vector3(0, 0, 300);
                if (ball.bombTime)
                {
                    catchImage.sprite = dodgeSprite;
                    helmet.SetActive(true);
                }
                else
                {
                    catchImage.sprite = catchSprite;
                    helmet.SetActive(false);
                }
            })
            .Append(catchText.DOAnchorPosX(0, 0.3f))
            .Join(catchText.DORotateQuaternion(new Quaternion(), 0.3f))
            .AppendInterval(0.3f)
            .Append(catchText.DOAnchorPosX(1000, 0.3f))
            .Join(catchText.DORotateQuaternion(new Quaternion(), 0.3f))
            .AppendCallback(KickBall);
    }

    private Vector2 GetSideToKick()
    {
        Vector2 side;
        if (sc.GetKickDirection().x > sc.GetKickPosition().x)
        {
            side = Vector2.left / 2;
            kickSprite = kickLeftLeg;
            ballRotationSpeed = -kickForce;
        }
        else
        {
            side = Vector2.right / 2;
            kickSprite = kickRightLeg;
            ballRotationSpeed = kickForce;
        }
        return side;
    }

    private void UpDifficulty()
    {
        kickForce += 0.5f;
        kickRotation += 0.5f;
        if (kickRotation > screenHalfWidth)
        {
            kickRotation = screenHalfWidth;
        }
    }

    public void OnPrepare()
    {
        if (image == null)
        {
            Init();
        }
        else
        {
            UpDifficulty();
        }
        image.sprite = idle;
        sc.MakeSpline(kickRotation);
        ball.gameObject.SetActive(true);
        ball.SetKickPosition(sc.GetKickPosition());
        transform.position = sc.GetKickPosition() + (Vector2.up / 2) + GetSideToKick();
        transform.up = sc.GetKickPosition() - (Vector2)transform.position;
    }

    private void KickBall()
    {
        image.sprite = kickSprite;

        ball.Kick(kickForce, ballRotationSpeed);

        Instantiate(kickEffect, GetEffectPos(), new Quaternion());
        Kick?.Invoke();
    }

    private Vector2 GetEffectPos()
    {
        return (transform.position + ball.transform.position) / 2;
    }

    public void OnGoalKeeperReady()
    {
        catchShow.Restart();
    }
}
