/**
 * @file JointManipulatorController.cs
 * @author Chen Chen (maker_cc@foxmail.com)
 * @brief Control orientation and position of manipulator
 * @version 0.1
 * @date 2022-09-24
 * 
 * @copyright Copyright IRM-Lab 2022
 */

using UnityEngine;
using MixedReality.Toolkit;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;

public class EndEffectorController : MonoBehaviour
{
    [SerializeField] private GameObject manipulatorCube;
    [SerializeField] private GameObject armBase;
    private ObjectManipulator objectManipulator;

    private Vector3 eePosition = Vector3.zero;

    private Quaternion eeRotation = Quaternion.identity;

    private readonly Vector3[] pathData = new Vector3[1000];

    private int i = 0;

    public float timeInterval = 50;

    public Vector3 eePositionValue
    {
        get => eePosition;
    }

    public Quaternion eeRotationValue
    {
        get => eeRotation;
    }

    void Start()
    {
        objectManipulator = GetComponent<ObjectManipulator>();
    }

    void Update()
    {
            // pointer tip pose in arm base coordinate
            // Debug.Log("position to world: " + manipulatorCube.transform.position);

            // eePose = manipulatorCube.transform.position - armBase.transform.position;
            eePosition = manipulatorCube.transform.localPosition;
            eePosition.y = (float)(eePosition.y - 0.1034);
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