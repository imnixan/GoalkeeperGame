using System.Collections;
using UnityEngine;

public class BallSprite : MonoBehaviour
{
    public float Rotation;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, Rotation);
    }
}
