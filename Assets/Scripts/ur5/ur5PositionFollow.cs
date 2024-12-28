/**
* @file ur5PositionFollow.cs
* @author ZoeQU
* @brief Let the UR5 follows a manipulator cube
* @version 0.1
* @date 2024-07-11
* 
* @copyright Copyright FLAIR 2024
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ur5PositionFollow : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    private Vector3 relativePos;
    private Quaternion relativeRot;

    // Start is called before the first frame update
    void Start()
    {
        relativePos = transform.position - targetObject.transform.position;
        relativeRot = transform.rotation * Quaternion.Inverse(targetObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        float targetRotY = targetObject.transform.rotation.eulerAngles.y;
        transform.SetPositionAndRotation(
            targetObject.transform.position + relativePos,
            Quaternion.Euler(0, targetRotY, 0) * relativeRot);
    }
}
