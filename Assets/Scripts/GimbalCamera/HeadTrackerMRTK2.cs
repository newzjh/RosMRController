using System;
using UnityEngine;

public class HeadTracker : MonoBehaviour
{
    private float pitchAngle; // pitch angle, from previous direction to current direction, in degrees
    public float PitchAngle => pitchAngle;
    private float azimuthAngle; // azimuth angle, from previous direction to current direction, in degrees
    public float AzimuthAngle => azimuthAngle;
    private Vector3 previousDirection = Vector3.forward;
    private Vector3 previousUp = Vector3.up;
    private Vector3 previousRight = Vector3.right;

    void Update()
    {
        Vector3 headGazeOrigin = Camera.main.transform.position;
        Vector3 headGazeDirection = Camera.main.transform.forward;
        Debug.DrawRay(headGazeOrigin, headGazeDirection * 10, Color.red);
        Debug.DrawRay(headGazeOrigin, previousDirection * 3, Color.green);
        Debug.DrawRay(headGazeOrigin, previousUp * 3, Color.blue);
        Debug.DrawRay(headGazeOrigin, previousRight * 3, Color.white);
        azimuthAngle = Vector3.SignedAngle(
            Vector3.ProjectOnPlane(headGazeDirection, previousUp),
            Vector3.ProjectOnPlane(previousDirection, previousUp),
            -previousUp
        );
        pitchAngle = Vector3.SignedAngle(
            Vector3.ProjectOnPlane(headGazeDirection, previousRight),
            Vector3.ProjectOnPlane(previousDirection, previousRight),
            previousRight
        );
        // Debug.Log(azimuthAngle + " " + pitchAngle);
    }

    public void CalibrateZero()
    {
        // reject if looking up/down
        if (Math.Abs(Vector3.Dot(Camera.main.transform.forward, Vector3.up)) > 0.9f)
        {
            return;
        }
        previousDirection = Camera.main.transform.forward;
        previousUp = Vector3.up;

        Vector3.OrthoNormalize(ref previousDirection, ref previousUp, ref previousRight);
    }
}
