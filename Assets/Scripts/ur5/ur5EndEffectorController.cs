/**
 * @file ur5EndEffectorController.cs
 * @author ZoeQU
 * @brief Control orientation and position of manipulator
 * @version 0.1
 * @date 2024-07-11
 * 
 * @copyright Copyright Flair 2024
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;

public class ur5EndEffectorController : MonoBehaviour
{
    [SerializeField] private GameObject manipulatorCube;
    [SerializeField] private GameObject armBase;
    private ObjectManipulator objectManipulator;

    private Vector3 eePosition = Vector3.zero;

    private Quaternion eeRotation = Quaternion.identity;

    private readonly Vector3[] pathData = new Vector3[1000];

    private int i = 0;
    private float h;
    public float timeInterval = 50;


    // eePositionValue 允许其他类或代码读取 eePosition 的值，但不能直接对其进行赋值。
    // 这可以确保 eePosition 只能在类内部进行修改，从而更好地控制和保护数据的完整性。
    public Vector3 eePositionValue
    {
        get => eePosition;
    }

    public Quaternion eeRotationValue
    {
        get => eeRotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        objectManipulator = GetComponent<ObjectManipulator>();
    }

    // Update is called once per frame
    void Update()
    {
        // pointer tip pose in arm base coordinate
        // Debug.Log("position to world: " + manipulatorCube.transform.position);

        // eePose = manipulatorCube.transform.position - armBase.transform.position;

        /// h is the bias of center = -0.4215486, only need to cal once;
        //Debug.Log(armBase.transform.position);
        //h = armBase.transform.position.y;
        //Debug.Log("h is " + h);

        eePosition = manipulatorCube.transform.localPosition;
        eePosition.y = (float)(eePosition.y - 0.1034); //TODO(): update "0.1034"
        // Debug.Log("Position in Unity: " + eePosition);
        // if(timeInterval > 0)
        // {
        //     timeInterval -= 1;
        // }
        // else
        // {
        //     Debug.Log(i);
        //     pathData[i] = eePosition;
        //     Debug.Log("Movement Path is: " + pathData[i]);
        //     i++;
        //     timeInterval = 50;
        // }

        // Quaternion eeRotation = Quaternion.Inverse(armBase.transform.rotation) * manipulatorCube.transform.localRotation;
        eeRotation = manipulatorCube.transform.localRotation;
        // Debug.Log("Orientation in Unity: " + eeRotation);
    }
}
