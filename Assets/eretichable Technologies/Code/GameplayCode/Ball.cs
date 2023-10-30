using System.Collections;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.Events;

public class Ball : MonoBehaviour, IPauseListener, ICatchListener
{
    public static event UnityAction GoalHappend;
    private SplineFollower sf;
    private Rigidbody2D rb;
    private BallSprite ballSprite;
    private float savedRotation;
    private bool savedFollow;
    private TrailRenderer trail;

    public void Init()
    {
        sf = GetComponent<SplineFollower>();
        sf.follow = false;
        rb = GetComponent<Rigidbody2D>();
        ballSprite = GetComponentInChildren<BallSprite>();
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
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
        trail.enabled = true;
    }

    public void OnCatchHappen()
    {
        gameObject.SetActive(false);
        trail.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            GoalHappend?.Invoke();
            ballSprite.Rotation = 0;
            trail.enabled = false;
        }
    }

    private void StopFollow()
    {
        sf.follow = false;
        trail.enabled = false;
    }

    public void OnPause()
    {
        savedFollow = sf.follow;
        sf.follow = false;
        savedRotation = ballSprite.Rotation;
        ballSprite.Rotation = 0;
    }

    public void Unpause()
    {
        sf.follow = savedFollow;
        ballSprite.Rotation = savedRotation;
    }
}
