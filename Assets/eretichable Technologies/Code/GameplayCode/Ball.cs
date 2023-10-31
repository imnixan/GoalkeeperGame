using System.Collections;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ball : MonoBehaviour, IPauseListener, ICatchListener
{
    public static event UnityAction GoalHappend;
    private SplineFollower sf;
    private Rigidbody2D rb;
    private BallSprite ballSprite;
    private float savedRotation;
    private bool savedFollow;
    public bool bombTime;

    [SerializeField]
    private Image image;

    [SerializeField]
    private ParticleSystem fire,
        greenFire;

    [SerializeField]
    private Sprite ball,
        bomb;

    public void Init()
    {
        sf = GetComponent<SplineFollower>();
        sf.follow = false;
        rb = GetComponent<Rigidbody2D>();
        ballSprite = GetComponentInChildren<BallSprite>();
        fire.Stop();
        greenFire.Stop();
    }

    public void SetKickPosition(Vector2 pos)
    {
        StopFollow();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        ballSprite.Rotation = 0;
        transform.position = pos;
        bombTime = Random.value > 0.6f;
        if (bombTime)
        {
            PrepareBomb();
        }
        else
        {
            PrepareBall();
        }
    }

    private void PrepareBomb()
    {
        tag = "Bomb";
        image.sprite = bomb;
    }

    private void PrepareBall()
    {
        tag = "Ball";
        image.sprite = ball;
    }

    public void Kick(float force, float rotation)
    {
        ballSprite.Rotation = rotation;
        sf.followSpeed = force;
        sf.follow = true;
        sf.Restart();
        rb.angularVelocity = 10;
        if (bombTime)
        {
            fire.Play();
        }
        else
        {
            greenFire.Play();
        }
    }

    public void OnCatchHappen()
    {
        greenFire.Stop();
        fire.Stop();
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            if (tag == "Ball")
            {
                Goal();
            }
            else if (tag == "Bomb")
            {
                FindAnyObjectByType<GoalKeeper>().Catch();
            }
            ballSprite.Rotation = 0;
            fire.Stop();
            greenFire.Stop();
        }
    }

    public void Goal()
    {
        GoalHappend?.Invoke();
        ballSprite.Rotation = 0;
        fire.Stop();
        greenFire.Stop();
        gameObject.SetActive(false);
    }

    private void StopFollow()
    {
        sf.follow = false;
        fire.Stop();
        greenFire.Stop();
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
