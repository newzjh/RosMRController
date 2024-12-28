/**
 * @file YumiEndEffectorController.cs
 * @author Zoe
 * @brief get Left End effector and Right End effector position
 * @version 0.1
 * @date 2023
 * 
 * @copyright Flair
 */

using UnityEngine;
using MixedReality.Toolkit;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;

public class YumiEndEffectorController : MonoBehaviour
{
    [SerializeField] private GameObject ManipulationCube;

    [SerializeField] private GameObject LeftEE;
    [SerializeField] private GameObject RightEE;

    [SerializeField] private GameObject armBase;
    private float h, j;

    private ObjectManipulator objectManipulator;
   
    private Vector3 LeePosition = Vector3.zero;
    private Vector3 ReePosition = Vector3.zero;

    private Quaternion LeeRotation = Quaternion.identity;
    private Quaternion ReeRotation = Quaternion.identity;

    private readonly Vector3[] pathData = new Vector3[1000];

    private int i = 0;

    public float timeInterval = 50;

    public Vector3 LeePositionValue
    {
        get => LeePosition;
    }

    public Quaternion LeeRotationValue
    {
        get => LeeRotation;
    }

    public Vector3 ReePositionValue
    {
        get => ReePosition;
    }

    public Quaternion ReeRotationValue
    {
        get => ReeRotation;
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

        /// h is the bias of center = -0.4215486, only need to cal once;
        //h = armBase.transform.position.y; 
        //Debug.Log("h is " + h);


        if (LeftEE != null) {
            LeePosition = LeftEE.transform.localPosition;
            LeePosition.y = (float)(LeePosition.y - (-0.4215486));
            LeeRotation = LeftEE.transform.localRotation;
        }

        if (RightEE != null)
        {
            ReePosition = RightEE.transform.localPosition;
            ReePosition.y = (float)(ReePosition.y - (-0.4215486));
            ReeRotation = RightEE.transform.localRotation;
        }


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

        // Debug.Log("Orientation in Unity: " + eeRotation);

    }

}