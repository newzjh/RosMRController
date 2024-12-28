/**
 * @file YumiPositionFollow.cs
 * @author zoequ
 * @brief Yumi follow the manipulation cube, follow Chen Chen (maker_cc@foxmail.com) work
 * @version 0.1
 * @date 2023
 * 
 * @copyright Copyright Flair 2023
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YumiPositionFollow : MonoBehaviour
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
