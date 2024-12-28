using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.UX;
using MixedReality.Toolkit.SpatialManipulation;

namespace AsImpL
{
    /// <summary>
    /// Examples for demonstrating AsImpL features.
    /// </summary>
    namespace Examples
    {
        /// <summary>
        /// Read a configuration file and load the object listed there with their parameters and positions.
        /// </summary>
        public class CustomObjImporter : MultiObjectImporter
        {
            //[Tooltip("Text for displaying the overall scale")]
            //public Text objScalingText;

            [Tooltip("Configuration XML file (relative to the application data folder)")]
            public string configFile = "../object_list.xml";

            private List<ModelImportInfo> modelsToImport = new List<ModelImportInfo>();

            [SerializeField] public GameObject leftGripper;
            [SerializeField] public GameObject rightGripper;
            private bool isCaptured;

            private GameObject targetObj;
            private bool isFollowing;

            private void Awake()
            {
                //Debug.Log("RootPath: " + RootPath);
                //Debug.Log("___configFile: " + configFile); //C:\Unity_tutorials\Version2.0-20240503\Assets\OBJImport\RuntimeModelImporter\models\object_list.xml
                
                configFile = RootPath + configFile;
                //Debug.Log("configFile: " + configFile);
            }


            protected override void Start()
            {
#if UNITY_STANDALONE
#if !UNITY_EDITOR
                string[] args = Environment.GetCommandLineArgs();

                if (args != null && args.Length > 1)
                {
                    int numImports = args.Length - 1;
                    for (int i = 0; i < numImports; i++)
                    {
                        if (args[i + 1].StartsWith("-"))
                        {
                            if (args[i + 1] == "-scale")
                            {
                                if (i + 1 < numImports)
                                {
                                    float.TryParse(args[i + 2], out defaultImportOptions.modelScaling);
                                }
                                i++;
                            }
                            continue;
                        }
                        ModelImportInfo modelToImport = new ModelImportInfo();
                        modelToImport.path = args[i + 1];
                        modelToImport.name = Path.GetFileNameWithoutExtension(modelToImport.path);
                        modelToImport.loaderOptions = new ImportOptions();
                        modelToImport.loaderOptions.modelScaling = defaultImportOptions.modelScaling;
                        modelToImport.loaderOptions.zUp = defaultImportOptions.zUp;
                        modelToImport.loaderOptions.reuseLoaded = false;
                        objectsList.Add(modelToImport);
                    }
                    configFile = "";

                    ImportModelListAsync(objectsList.ToArray());
                }
                else
#endif
                {
                    if (autoLoadOnStart)
                    {
                         //Reload();
                    }
                }
#else
                Debug.Log("Command line arguments not available, using default settings.");
                Reload();
#endif
            }


            public void SetScaling(float scl)
            {
                scl = Mathf.Pow(1.0f, scl);
                //objScalingText.text = "Scaling: " + scl;
                transform.localScale = new Vector3(scl, scl, scl);
            }


            protected override void OnImportingComplete()
            {
                base.OnImportingComplete();
                UpdateScene();
            }


            public void Save()
            {
                if (!allLoaded || string.IsNullOrEmpty(configFile))
                {
                    return;
                }

                UpdateObjectList();
                XmlSerializer serializer = new XmlSerializer(objectsList.GetType());
                FileStream stream = new FileStream(configFile, FileMode.Create);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
                settings.Encoding = Encoding.UTF8;
                settings.CheckCharacters = true;

                XmlWriter w = XmlWriter.Create(stream, settings);

                serializer.Serialize(w, objectsList);
                stream.Dispose();
            }


            public void Reload()
            {
                if (string.IsNullOrEmpty(configFile))
                {
                    Debug.LogWarning("Empty configuration file path");
                    return;
                }

                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<ModelImportInfo>));
                    FileStream stream = new FileStream(configFile, FileMode.Open);
                    objectsList = (List<ModelImportInfo>)serializer.Deserialize(stream);
                    stream.Dispose();
                    UpdateScene();
                    ImportModelListAsync(modelsToImport.ToArray());

                    // object follow
                    StartCoroutine(DelayedTargetObjFollow());
                }
                catch (IOException e)
                {
                    Debug.LogWarningFormat("Unable to open configuration file {0}: {1}", configFile, e);
                }
            }

            private System.Collections.IEnumerator DelayedTargetObjFollow()
            {
                while (true)
                {
                    TargetObjFollow();
                    yield return new WaitForSeconds(0.1f);

                }
            }

            private void UpdateObject(GameObject gameObj, ModelImportInfo importInfo)
            {
                gameObj.name = importInfo.name;
                //game_object.transform.localScale = scale;
                if (importInfo.loaderOptions != null)
                {
                    gameObj.transform.localPosition = importInfo.loaderOptions.localPosition;
                    gameObj.transform.localRotation = Quaternion.Euler(importInfo.loaderOptions.localEulerAngles);
                    gameObj.transform.localScale = importInfo.loaderOptions.localScale;

                    // Add a BoxCollider if none exists
                    if (gameObj.GetComponent<Collider>() == null)
                    {
                        gameObj.AddComponent<BoxCollider>();
                    }

                    // Add interactive components for the loaded object
                    //if (gameObj.GetComponent<NearInteractionGrabbable>() == null)
                    //{
                    //    gameObj.AddComponent<NearInteractionGrabbable>();
                    //}

                    // Add a Rigidbody if needed for physics-based interactions
                    if (gameObj.GetComponent<Rigidbody>() == null)
                    {
                        gameObj.AddComponent<Rigidbody>().isKinematic = true;
                    }

                    // Add ManipulationHandler for advanced interactions
                    //if (gameObj.GetComponent<ManipulationHandler>() == null)
                    //{
                    //    gameObj.AddComponent<ManipulationHandler>();
                    //}

                }
            }


            private void UpdateImportInfo(ModelImportInfo importInfo, GameObject gameObj)
            {
                importInfo.name = gameObj.name;
                if (importInfo.loaderOptions == null)
                {
                    importInfo.loaderOptions = new ImportOptions();
                }
                importInfo.loaderOptions.localPosition = gameObj.transform.localPosition;
                importInfo.loaderOptions.localEulerAngles = gameObj.transform.localRotation.eulerAngles;
                importInfo.loaderOptions.localScale = gameObj.transform.localScale;
            }


            private void UpdateObjectList()
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform tr = transform.GetChild(i);
                    ModelImportInfo info = objectsList.Find(obj => obj.name == tr.name);
                    if (info != null)
                    {
                        UpdateImportInfo(info, tr.gameObject);
                    }
                }
            }


            private void UpdateScene()
            {
                modelsToImport.Clear();
                List<string> names = new List<string>();
                // add or update objects that are present in the list
                foreach (ModelImportInfo info in objectsList)
                {
                    // Debug.Log("*****************info.name: " + info.name); //

                    names.Add(info.name);
                    Transform transf = transform.Find(info.name);
                    if (transf == null)
                    {
                        modelsToImport.Add(info);
                    }
                    else
                    {
                        UpdateObject(transf.gameObject,info);
                    }
                }
                // destroy objects that are not present in the list
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform tr = transform.GetChild(i);
                    if (tr.gameObject != gameObject && !names.Contains(tr.gameObject.name))
                    {
                        Destroy(tr.gameObject);
                    }
                }

                // debug and output
                Debug.Log("Current objects in scene:");
                foreach (Transform t in transform)
                {
                    Debug.Log(t.name);
                }
            }


            public void HideAllMesh() 
            {
                modelsToImport.Clear();
                List<string> names = new List<string>();
                // add or update objects that are present in the list
                foreach (ModelImportInfo info in objectsList)
                {
                    // Debug.Log("*****************info.name: " + info.name); //

                    names.Add(info.name);
                    Transform transf = transform.Find(info.name);
                    if (transf == null)
                    {
                        // TODO: 
                    }
                    else
                    {
                        Destroy(transf.gameObject);
                    }
                }
                // destroy objects that are not present in the list
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform tr = transform.GetChild(i);
                    if (tr.gameObject != gameObject && !names.Contains(tr.gameObject.name))
                    {
                        Destroy(tr.gameObject);
                    }
                }
            }

            // from here, judge if the target obj has been captured or not
            void TargetObjFollow()
            {
                targetObj = GameObject.Find("BumpRefl"); // the tragetObj name.
                isFollowing = false;

                if (targetObj != null)
                {
                    Debug.Log("found the target: BumpRefl");
                    // if overlap, left_gripper / right_gripper 
                    isCaptured = true; // need further develop
                    bool overlapsWithLeft = IsOverlapping(targetObj, leftGripper);
                    //Debug.Log("overlapsWithLeft：" + overlapsWithLeft);

                    bool overlapsWithRight = IsOverlapping(targetObj, rightGripper);
                    //Debug.Log("overlapsWithRight: " + overlapsWithRight);


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
                else
                {
                    Debug.Log("Objcet not found!!!");
                }
            }

            bool IsOverlapping(GameObject a, GameObject b)
            {
                Collider colliderA = a.GetComponent<Collider>();
                Collider colliderB = b.GetComponent<Collider>();

                if (colliderA == null || colliderB == null)
                {
                    Debug.LogWarning("One of the objects is missing a Collider component.");
                    return false;
                }

                bool isOverlapping = colliderA.bounds.Intersects(colliderB.bounds);
                Debug.Log($"IsOverlapping called: {isOverlapping}");
                return isOverlapping;
            }

            void Follow(GameObject target, GameObject gripper)
            {
                // follow
                Debug.Log("Follow called");
                target.transform.position = gripper.transform.position;
            }
        }
    }
}
