using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         Mesh mesh = new Mesh();
         mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
