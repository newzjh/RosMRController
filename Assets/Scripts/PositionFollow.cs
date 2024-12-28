/**
 * @file PositionFollow.cs
 * @author Chen Chen (maker_cc@foxmail.com)
 * @brief Let the object follows a manipulator cube
 * @version 0.1
 * @date 2022-09-24
 * 
 * @copyright Copyright IRM-Lab 2022
 */

using UnityEngine;

public class PositionFollow : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    private Vector3 relativePos;
    private Quaternion relativeRot;

    void Start()
    {
        relativePos = transform.position - targetObject.transform.position;
        relativeRot = transform.rotation * Quaternion.Inverse(targetObject.transform.rotation);
    }

    void Update()
    {
        float targetRotY = targetObject.transform.rotation.eulerAngles.y;
        transform.SetPositionAndRotation(
            targetObject.transform.position + relativePos,
            Quaternion.Euler(0, targetRotY, 0) * relativeRot);
    }
}
