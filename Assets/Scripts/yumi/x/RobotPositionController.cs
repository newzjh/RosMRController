using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPositionController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;  //目标物体的transform组件
    public float speed = 0.01f;  //平滑移动的速度
    void Start()
    {
        target = GameObject.FindWithTag("Target").transform; //获取目标物体的 Transform 组件
    }

    // Update is called once per frame
    void Update()
    {
    
        transform.position = target.position; //将当前物体的位置设置为目标物体的位置
        transform.position = Vector3.Lerp(transform.position, transform.position, speed); //平滑移动

    }
}
