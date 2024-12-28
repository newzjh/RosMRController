/**
 * @file ConstraintActivator.cs
 * @author Chen Chen (maker_cc@foxmail.com)
 * @brief Active constraints using a panel
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
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ConstraintActivator : MonoBehaviour
{
    [SerializeField] private GameObject positionToggleButton;
    [SerializeField] private GameObject rotationToggleButton;
    private MoveAxisConstraint posConstraint;
    private RotationAxisConstraint rotConstraint;
    public AxisFlags rotConstraintAxis;

    void Start()
    {
        posConstraint = GetComponent<MoveAxisConstraint>();
        rotConstraint = GetComponent<RotationAxisConstraint>();
    }

    void Update()
    {
        if (posConstraint == null || rotConstraint == null)
            return;

        rotConstraintAxis = rotConstraint.ConstraintOnRotation;
        switch (rotationToggleButton.GetComponent<PressableButton>().isSelected)
        {
            case false:
                rotConstraint.ConstraintOnRotation &= ~AxisFlags.YAxis; //remove
                break;

            case true:
                rotConstraint.ConstraintOnRotation |= AxisFlags.YAxis; //add
                break;
        }
        switch (positionToggleButton.GetComponent<PressableButton>().isSelected)
        {
            case false:
                posConstraint.enabled = false;
                break;

            case true:
                posConstraint.enabled = true;
                break;
        }
        //hide manip cube when constraints are all toggled
        if (posConstraint.enabled == true && (rotConstraint.ConstraintOnRotation & AxisFlags.YAxis) != 0)
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<ObjectManipulator>().enabled = false;
            if (GetComponent<XRBaseInteractable>())
                GetComponent<XRBaseInteractable>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<ObjectManipulator>().enabled = true;
            if (GetComponent<XRBaseInteractable>())
                GetComponent<XRBaseInteractable>().enabled = true;
        }
    }
}
