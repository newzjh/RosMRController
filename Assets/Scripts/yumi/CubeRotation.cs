/**
 * @file CubeRotation.cs 
 * @author zoequ
 * @brief rotate Yumi by controlling ManipulatorCube, for debug
 * @version 0.1
 * @date 2023
 * 
 * @copyright Copyright Flair 2023
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float rotationSpeed = 50.0f;
    private float rotationAmount = 20.0f; // 已经旋转的角度

    // 开始旋转
    void StartRotation_clockwise()
    {
        //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // 绕y轴旋转
        transform.Rotate(Vector3.up, rotationAmount); // 绕y轴旋转
    }

    void StartRotation_Counterclockwise()
    {
        //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // 绕y轴旋转
        transform.Rotate(Vector3.up, -rotationAmount); // 绕y轴旋转
    }

    // 停止旋转
    void StopRotation()
    {
        transform.Rotate(Vector3.zero); // 将旋转角度设为0，停止旋转
    }

// Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartRotation_clockwise(); // 如果按下C键，则开始顺时针旋转
        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            StartRotation_Counterclockwise(); // 如果按下C键，则开始逆时针旋转
        }

        else
        {
            StopRotation(); // 否则停止旋转
        }
    }
}

