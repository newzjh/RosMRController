/**
 * @file YumiConstraintActivator.cs
 * @author zoequ
 * @brief Active constraints using a panel, follow Chen Chen (maker_cc@foxmail.com) work
 * @version 0.1
 * @date 2023
 * 
 * @copyright Copyright Flair 2023
 */

using UnityEngine;
using MixedReality.Toolkit;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;

public class YumiConstraintActivator : MonoBehaviour
{
    [SerializeField] private GameObject positionToggleButton;
    [SerializeField] private GameObject rotationToggleButton;
    private MoveAxisConstraint posConstraint;
    private RotationAxisConstraint rotConstraint;
    public AxisFlags rotConstraintAxis;

    // Start is called before the first frame update
    void Start()
    {
        posConstraint = GetComponent<MoveAxisConstraint>();
        rotConstraint = GetComponent<RotationAxisConstraint>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotConstraintAxis = rotConstraint.ConstraintOnRotation;

        //switch (rotationToggleButton.GetComponent<Interactable>().CurrentDimension)
        //{
        //    case 0:
        //        rotConstraint.ConstraintOnRotation &= ~AxisFlags.YAxis; //remove
        //        break;

        //    case 1:
        //        rotConstraint.ConstraintOnRotation |= AxisFlags.YAxis; //add
        //        break;
        //}
        //switch (positionToggleButton.GetComponent<Interactable>().CurrentDimension)
        //{
        //    case 0:
        //        posConstraint.enabled = false;
        //        break;

        //    case 1:
        //        posConstraint.enabled = true;
        //        break;
        //}


        ////hide manip cube when constraints are all toggled
        //if (posConstraint.enabled == true && (rotConstraint.ConstraintOnRotation & AxisFlags.YAxis) != 0)
        //{
        //    GetComponent<Renderer>().enabled = false;
        //    GetComponent<ObjectManipulator>().enabled = false;
        //    //GetComponent<NearInteractionGrabbable>().enabled = false;
        //}
        //else
        //{
        //    GetComponent<Renderer>().enabled = true;
        //    GetComponent<ObjectManipulator>().enabled = true;
        //    //GetComponent<NearInteractionGrabbable>().enabled = true;
        //}
    }
}
