/**
 * @file ManipObjAutoReturnMid.cs
 * @author Chen Chen (maker_cc@foxmail.com)
 * @brief Let manipulator return mid position after release
 * @version 0.1
 * @date 2022-09-24
 * 
 * @copyright Copyright IRM-Lab 2022
 */

using UnityEngine;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;

public class ManipObjAutoReturnMid : MonoBehaviour
{
    [SerializeField] private GameObject manipObj;
    private ObjectManipulator objectManipulator;

    void Start()
    {
        objectManipulator = GetComponent<ObjectManipulator>();
        //objectManipulator.onLastHoverExited.AddListener(ManipObjReturnMid);
    }

    //public void ManipObjReturnMid(ManipulationEventData data)
    //{
    //    manipObj.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
    //}

}
