using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoordinates : MonoBehaviour
{
    [SerializeField] private GameObject YumiBody;
    [SerializeField] private GameObject InsideLeftEE;
    [SerializeField] private GameObject OutsideLeftEE;
    [SerializeField] private GameObject InsideRightEE;
    [SerializeField] private GameObject OutsideRightEE;

    // Start is called before the first frame update
    void Start()
    {
        // 获取 YumiBody 的坐标
        Vector3 yumiBodyPosition = YumiBody.transform.position;
        Debug.Log("YumiBody 坐标：" + yumiBodyPosition);

        // 获取 InsideLeftEE 的坐标
        Vector3 insideLeftEEPosition = InsideLeftEE.transform.position;
        Debug.Log("InsideLeftEE 坐标：" + insideLeftEEPosition);

        // 获取 OutsideLeftEE 的坐标
        Vector3 outsideLeftEEPosition = OutsideLeftEE.transform.position;
        Debug.Log("OutsideLeftEE 坐标：" + outsideLeftEEPosition);

        // 获取 InsideLeftEE 的坐标
        Vector3 insideRightEEPosition = InsideRightEE.transform.position;
        Debug.Log("InsideLeftEE 坐标：" + insideLeftEEPosition);

        // 获取 OutsideLeftEE 的坐标
        Vector3 outsideRightEEPosition = OutsideRightEE.transform.position;
        Debug.Log("OutsideLeftEE 坐标：" + outsideLeftEEPosition);

        // 添加一个cube至（0，0，0）
        //AddCube();
    }

    // 添加Cube至（0，0，0）
    void AddCube()
    {
        // 创建Cube物体
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // 设置Cube的位置
        cube.transform.position = Vector3.zero;

        // 添加默认材质
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material = new Material(Shader.Find("Standard"));

        // 将Cube物体设置为当前对象的子物体
        cube.transform.SetParent(transform);
    }
}
