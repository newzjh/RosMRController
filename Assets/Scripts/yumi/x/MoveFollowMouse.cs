using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFollowMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //首先获取到当前物体的屏幕坐标
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

        //让鼠标的屏幕坐标的Z轴等于当前物体的屏幕坐标的Z轴，也就是相隔的距离
        Vector3 m_MousePos = new Vector3(Input.mousePosition.x, pos.y, Input.mousePosition.y);
        //Vector3 m_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);

        //将正确的鼠标屏幕坐标换成世界坐标交给物体
        transform.position = Camera.main.ScreenToWorldPoint(m_MousePos);
    }
}