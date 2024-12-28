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
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;

public class JointManipulatorController : MonoBehaviour
{
    [SerializeField] private GameObject manipPointerTip;
    [SerializeField] private float pointerScale;
    [SerializeField] private GameObject armBase;
    private GameObject pointerCylinder;
    private ObjectManipulator objectManipulator;

    private Vector3 manipValue = Vector3.zero;
    public Vector3 ManipValue
    {
        get => manipValue;
    }

    void Start()
    {
        objectManipulator = GetComponent<ObjectManipulator>();
        //objectManipulator.OnManipulationStarted.AddListener(DisplayPointer);
        //objectManipulator.OnManipulationEnded.AddListener(ManipPointerReturnMid);

        pointerCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        pointerCylinder.SetActive(false);
        manipPointerTip.SetActive(false);
    }

    void Update()
    {
        if (pointerCylinder.activeInHierarchy)
        {
            Vector3 deltaPos = manipPointerTip.transform.position - transform.position;
            Vector3 cylinderPos = (this.transform.position + manipPointerTip.transform.position) / 2;
            Quaternion tipRot = Quaternion.FromToRotation(-Vector3.forward, deltaPos);
            Quaternion cylinderRot = Quaternion.FromToRotation(Vector3.up, deltaPos);
            Vector3 cylinderScale = new Vector3(pointerScale, deltaPos.magnitude / 2, pointerScale);
            pointerCylinder.transform.position = cylinderPos;
            pointerCylinder.transform.rotation = cylinderRot;
            pointerCylinder.transform.localScale = cylinderScale;
            manipPointerTip.transform.rotation = tipRot; //draw arrow

            manipValue = Quaternion.Inverse(armBase.transform.rotation) * deltaPos; //return vel vector in base coordinate
        }
    }

    //public void ManipPointerReturnMid(ManipulationEventData data)
    //{
    //    manipPointerTip.transform.SetPositionAndRotation(transform.position, transform.rotation);
    //    manipPointerTip.SetActive(false);
    //    pointerCylinder.transform.localScale = Vector3.zero;
    //    pointerCylinder.SetActive(false);
    //    manipValue = Vector3.zero;
    //}

    //public void DisplayPointer(ManipulationEventData data)
    //{
    //    manipPointerTip.SetActive(true);
    //    pointerCylinder.SetActive(true);
    //}
}