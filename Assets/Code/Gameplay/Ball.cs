using System.Collections;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    public static event UnityAction GoalHappend;
    private SplineFollower sf;
    private Rigidbody2D rb;
    private BallSprite ballSprite;

    public void Init()
    {
        sf = GetComponent<SplineFollower>();
        sf.follow = false;
        rb = GetComponent<Rigidbody2D>();
        ballSprite = GetComponentInChildren<BallSprite>();
    }

    public void SetKickPosition(Vector2 pos)
    {
        StopFollow();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        ballSprite.Rotation = 0;
        transform.position = pos;
    }

    public void Kick(float force, float rotation)
    {
        ballSprite.Rotation = rotation;
        sf.followSpeed = force;
        sf.follow = true;
        sf.Restart();
        rb.angularVelocity = 10;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            GoalHappend?.Invoke();
            ballSprite.Rotation = 0;
        }
    }

    private void StopFollow()
    {
        sf.follow = false;
        StopAllCoroutines();
    }
}
