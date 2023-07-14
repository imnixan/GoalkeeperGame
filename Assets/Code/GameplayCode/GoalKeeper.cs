using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalKeeper : MonoBehaviour, IPreparingListener, IPauseListener
{
    public static event UnityAction CatchBall;
    public static event UnityAction GoalKeeperReady;
    private RectTransform rt;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private bool grabbed;
    private RectTransform parentRt;
    private Vector2 FieldSize;
    private Camera camera;
    private Vector3[] fieldCorners;
    private GameObject ballInHand;

    private void Init()
    {
        rt = GetComponent<RectTransform>();
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.fixedAngle = true;
        coll = gameObject.AddComponent<BoxCollider2D>();
        coll.size = rt.sizeDelta;
        parentRt = transform.parent.GetComponent<RectTransform>();
        camera = Camera.main;
        fieldCorners = new Vector3[4];
        parentRt.GetWorldCorners(fieldCorners);
        ballInHand = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (grabbed)
        {
            rb.MovePosition(GetMovePosition());
        }
    }

    private Vector2 GetMovePosition()
    {
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.y > fieldCorners[1].y)
        {
            mousePos.y = fieldCorners[1].y;
        }
        return mousePos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            CatchBall?.Invoke();
            ballInHand.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        GoalKeeperReady?.Invoke();
        grabbed = true;
    }

    private void OnMouseUp()
    {
        grabbed = false;
    }

    public void OnPrepare()
    {
        if (ballInHand == null)
        {
            Init();
        }
        ballInHand.SetActive(false);
    }

    public void OnPause()
    {
        grabbed = false;
        coll.enabled = false;
    }

    public void Unpause()
    {
        coll.enabled = true;
    }
}
