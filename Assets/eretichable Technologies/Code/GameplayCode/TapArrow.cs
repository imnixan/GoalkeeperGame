using System.Collections;
using UnityEngine;

public class TapArrow : MonoBehaviour
{
    [SerializeField]
    private Transform goalkeeper;

    private void FixedUpdate()
    {
        transform.right = goalkeeper.position - transform.position;
    }
}
