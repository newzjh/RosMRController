using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjFollower : MonoBehaviour
{
    [SerializeField] public GameObject leftGripper;
    [SerializeField] public GameObject rightGripper;
    private bool isCaptured;

    private GameObject targetObj;
    private bool isFollowing;


    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.Find("testBumpRefl");
        isFollowing = false;
    }

    // Update is called once per frame
    void TargetObjFollow()
    {
        targetObj = GameObject.Find("testBumpRefl");
        isFollowing = false;

        if (targetObj != null)
        {
            // if overlap, left_gripper / right_gripper 
            bool overlapsWithLeft = IsOverlapping(targetObj, leftGripper);
            bool overlapsWithRight = IsOverlapping(targetObj, rightGripper);

            if ((overlapsWithLeft || overlapsWithRight) && isCaptured)
            {
                // start following
                isFollowing = true;
            }

            if (isFollowing)
            {
                if (overlapsWithLeft)
                {
                    Follow(targetObj, leftGripper);
                }
                else if (overlapsWithRight)
                {
                    Follow(targetObj, rightGripper);
                }
            }
        }
    }
    bool IsOverlapping(GameObject a, GameObject b)
    {
        // if overlap
        return a.GetComponent<Collider>().bounds.Intersects(b.GetComponent<Collider>().bounds);
    }

    void Follow(GameObject target, GameObject gripper)
    {
        // follow
        target.transform.position = gripper.transform.position;
    }
}
