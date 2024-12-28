using System.Collections;
using System.Collections.Generic;
using Dummiesman;
using UnityEngine;
using System.IO;


public class TestingRuntimeImporter : MonoBehaviour
{
    public string objPath = string.Empty;
    string objPathCurrent = string.Empty;
    string error = string.Empty;
    bool haveError = false;
    GameObject loadedObject;

    void Update()
    {
        if (objPathCurrent != objPath || haveError == true)
        {
            if (!File.Exists(objPath))
            {
                error = "File doesn't exist.";
                haveError = true;
                Debug.Log(error);
            }

            else
            {
                if (loadedObject != null)
                    Destroy(loadedObject);
                loadedObject = new OBJLoader().Load(objPath);
                objPathCurrent = objPath;
                haveError = false;
                error = string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(error))
            {
                Debug.Log(error);
            }
        }
    }

    // void OnGUI() {
    //     objPath = GUI.TextField(new Rect(0, 0, 256, 32), objPath);

    //     GUI.Label(new Rect(0, 0, 256, 32), "Obj Path:");
    //     if(GUI.Button(new Rect(256, 32, 64, 32), "Load File"))
    //     {
    //         //file path
    //         if (!File.Exists(objPath))
    //         {
    //             error = "File doesn't exist.";
    //         }else{
    //             if(loadedObject != null)            
    //                 Destroy(loadedObject);
    //             loadedObject = new OBJLoader().Load(objPath);
    //             error = string.Empty;
    //         }
    //     }

    //     if(!string.IsNullOrWhiteSpace(error))
    //     {
    //         GUI.color = Color.red;
    //         GUI.Box(new Rect(0, 64, 256 + 64, 32), error);
    //         GUI.color = Color.white;
    //     }
    // }
}