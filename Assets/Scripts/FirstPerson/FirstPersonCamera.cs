using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    private Camera cam;
    public Vector3 offset = Vector3.zero;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam && target)
        {
            Vector3 newpos = target.position;
            newpos = cam.transform.position + offset;
            newpos.y = target.position.y;
            target.position = newpos;
        }
    }
}
