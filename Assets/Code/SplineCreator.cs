using System.Collections;
using UnityEngine;
using Dreamteck.Splines;

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

    public void Init()
    {
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
                spline.GetPointPosition(1).y,
                zCoord
            )
        );
    }

    private float GetXCoord()
    {
        return Random.Range(screenCorners[LefdDownAngle].x, screenCorners[RightDownAnlge].x);
    }

    private float GetRotationX(float startPoint, float endPoint, float rotateMax)
    {
        float rotPointX = (startPoint + endPoint) / 2;
        rotPointX += Random.Range(-rotateMax, rotateMax);
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
