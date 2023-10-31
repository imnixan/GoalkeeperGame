using System.Collections;
using UnityEngine;
using Dreamteck.Splines;
using System.Runtime.CompilerServices;

public class SplineCreator : MonoBehaviour
{
    [SerializeReference]
    private RectTransform parent;

    private SplineComputer spline;
    private Vector3[] screenCorners = new Vector3[4];
    private float zCoord;
    private const int LefdDownAngle = 0;
    private const int LeftUpAngle = 1;
    private const int RightUpAngle = 2;
    private const int RightDownAnlge = 3;
    private const int KickPoint = 0;
    private const int RotationPoint = 1;
    private const int GoalPoint = 2;
    private Vector3 screenSize;

    public void Init()
    {
        screenSize = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
        parent.GetWorldCorners(screenCorners);
        spline = GetComponent<SplineComputer>();
        zCoord = spline.GetPointPosition(0).z;
    }

    public void MakeSpline(float rotation)
    {
        float kickXCoord = GetXCoord();
        spline.SetPointPosition(
            KickPoint,
            new Vector3(kickXCoord, screenCorners[LeftUpAngle].y - 1, zCoord)
        );

        float goalXCoord = GetXCoord();
        spline.SetPointPosition(
            GoalPoint,
            new Vector3(goalXCoord, screenCorners[LefdDownAngle].y, zCoord)
        );

        spline.SetPointPosition(
            RotationPoint,
            new Vector3(
                GetRotationX(kickXCoord, goalXCoord, rotation),
                spline.GetPointPosition(RotationPoint).y + Random.Range(-0.5f, 1f),
                zCoord
            )
        );
    }

    private float GetXCoord()
    {
        return Random.Range(
            screenCorners[LefdDownAngle].x + 0.5f,
            screenCorners[RightDownAnlge].x - 0.5f
        );
    }

    private float GetRotationX(float startPoint, float endPoint, float rotateMax)
    {
        float rotPointX = (startPoint + endPoint) / 2;
        rotPointX += Random.Range(-rotateMax, rotateMax);
        if (rotPointX < -screenSize.x)
        {
            rotPointX = -screenSize.x;
        }
        if (rotPointX > screenSize.x)
        {
            rotPointX = screenSize.x;
        }
        return rotPointX;
    }

    public Vector2 GetKickDirection()
    {
        return spline.GetPointPosition(1);
    }

    public Vector2 GetKickPosition()
    {
        return spline.GetPointPosition(0);
    }
}
