//MIT License
//Copyright (c) 2020 Mohammed Iqubal Hussain
//Website : Polyandcode.com 

using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace PolyAndCode.UI
{

    /// <summary>
    /// Entry for the recycling system. Extends Unity's inbuilt ScrollRect.
    /// </summary>
    public class RecyclableScrollRect : ScrollRect
    {
        [HideInInspector]
        public IRecyclableScrollRectDataSource DataSource;

        public bool IsGrid;
        //Prototype cell can either be a prefab or present as a child to the content(will automatically be disabled in runtime)
        public RectTransform PrototypeCell;
        public GameObject target;
        public GameObject trackPanel;
        public TMP_Dropdown toolbar;
        //If true the intiziation happens at Start. Controller must assign the datasource in Awake.
        //Set to false if self init is not required and use public init API.
        public bool SelfInitialize = true;

        // ----------------------------- personal -----------------------------
        // public Button AddButton;
        // public Button RemoveButton;
        // public Button ClearButton;
        
        public int visibledataLength;
        public int dataLength;
        public List<Point> pointList;
        // ----------------------------- personal -----------------------------

        public enum DirectionType
        {
            Vertical,
            Horizontal
        }

        public DirectionType Direction;

        //Segments : coloums for vertical and rows for horizontal.
        public int Segments
        {
            set
            {
                _segments = Math.Max(value, 2);
            }
            get
            {
                return _segments;
            }
        }
        [SerializeField]
        private int _segments;

        private RecyclingSystem _recyclingSystem;
        private Vector2 _prevAnchoredPos;

        protected override void Start()
        {
            //defafult(built-in) in scroll rect can have both directions enabled, Recyclable scroll rect can be scrolled in only one direction.
            //setting default as vertical, Initialize() will set this again. 
            vertical = true;
            horizontal = false;
            target = GameObject.Find("panda_hand_controller").gameObject;
            trackPanel = GameObject.Find("trackBar").gameObject;
            
            GameObject dropdownObject = GameObject.Find("functions");
            // toolbar = dropdownObject.GetComponent<TMP_Dropdown>();
            if (dropdownObject != null)
            {
                toolbar = dropdownObject.GetComponent<TMP_Dropdown>();
                if (toolbar == null)
                {
                    Debug.LogError("TMP_Dropdown component not found on the GameObject!");
                }
            }
            else
            {
                Debug.LogError("GameObject with the name 'YourDropdownName' not found!");
            }
            toolbar.onValueChanged.AddListener(showUp);
            
            if (!Application.isPlaying) return;
            if (SelfInitialize) Initialize();
            trackPanel.SetActive(false);
        }

        /// <summary>
        /// Initialization when selfInitalize is true. Assumes that data source is set in controller's Awake.
        /// </summary>
        private void Initialize()
        {
            //Contruct the recycling system.
            if (Direction == DirectionType.Vertical)
            {
                _recyclingSystem = new VerticalRecyclingSystem(PrototypeCell, viewport, content, DataSource, IsGrid, Segments);
            }
            else if (Direction == DirectionType.Horizontal)
            {
                _recyclingSystem = new HorizontalRecyclingSystem(PrototypeCell, viewport, content, DataSource, IsGrid, Segments);
            }
            vertical = Direction == DirectionType.Vertical;
            horizontal = Direction == DirectionType.Horizontal;

            _prevAnchoredPos = content.anchoredPosition;
            onValueChanged.RemoveListener(OnValueChangedListener);
            //Adding listener after pool creation to avoid any unwanted recycling behaviour.(rare scenerio)
            StartCoroutine(_recyclingSystem.InitCoroutine(() =>
                                                               onValueChanged.AddListener(OnValueChangedListener)
                                                              ));
        }

        /// <summary>
        /// public API for Initializing when datasource is not set in controller's Awake. Make sure selfInitalize is set to false. 
        /// </summary>
        public void Initialize(IRecyclableScrollRectDataSource dataSource)
        {
            DataSource = dataSource;
            Initialize();
        }

        /// <summary>
        /// Added as a listener to the OnValueChanged event of Scroll rect.
        /// Recycling entry point for recyling systems.
        /// </summary>
        /// <param name="direction">scroll direction</param>
        public void OnValueChangedListener(Vector2 normalizedPos)
        {
            Vector2 dir = content.anchoredPosition - _prevAnchoredPos;
            m_ContentStartPosition += _recyclingSystem.OnValueChangedListener(dir);
            _prevAnchoredPos = content.anchoredPosition;
        }

        /// <summary>
        ///Reloads the data. Call this if a new datasource is assigned.
        /// </summary>
        public void ReloadData()
        {
            ReloadData(DataSource);
        }

        /// <summary>
        /// Overloaded ReloadData with dataSource param
        ///Reloads the data. Call this if a new datasource is assigned.
        /// </summary>
        public void ReloadData(IRecyclableScrollRectDataSource dataSource)
        {
            if (_recyclingSystem != null)
            {
                StopMovement();
                onValueChanged.RemoveListener(OnValueChangedListener);
                _recyclingSystem.DataSource = dataSource;
                StartCoroutine(_recyclingSystem.InitCoroutine(() =>
                                                               onValueChanged.AddListener(OnValueChangedListener)
                                                              ));
                _prevAnchoredPos = content.anchoredPosition;
            }
        }


        // ----------------------------- personal -----------------------------
        public void AddPoint()
        {
            Debug.Log("AddPoint:" + visibledataLength + " " + dataLength);
            Debug.Log("Target:" + target.transform.position.x + " " + target.transform.position.y + " " + target.transform.position.z);
            visibledataLength = DataSource.GetVisibledataLength();
            dataLength = DataSource.GetdataLength();
            Debug.Log("AddPoint:" + visibledataLength + " " + dataLength);
            Debug.Log("Target:" + target.transform.position.x + " " + target.transform.position.y + " " + target.transform.position.z);
            if (visibledataLength < dataLength)
            {
                visibledataLength++;
                pointList= DataSource.GetPointList();
                Point newpoint = pointList[visibledataLength - 1];
                newpoint.x = target.transform.position.x;
                newpoint.y = target.transform.position.y;
                newpoint.z = target.transform.position.z;
                newpoint.alpha = target.transform.rotation.eulerAngles.x;
                newpoint.beta = target.transform.rotation.eulerAngles.y;
                newpoint.gamma = target.transform.rotation.eulerAngles.z;
                pointList[visibledataLength - 1] = newpoint;
                DataSource.UpdatePointList(pointList, visibledataLength);
            }
            ReloadData();
        }

        // bind point remove to button
        public void RemovePoint()
        {
            visibledataLength = DataSource.GetVisibledataLength();
            if (visibledataLength > 0)
            {
                visibledataLength--;
                DataSource.setVisibledatalength(visibledataLength);
            }
            ReloadData();
        }

        // bind point clear to button
        public void ClearPoint()
        {
            visibledataLength = DataSource.GetVisibledataLength();
            visibledataLength=0;
            DataSource.setVisibledatalength(visibledataLength);
            ReloadData();
        }

        // export point list to csv
        public void export()
        {
            pointList = DataSource.GetPointList();
            visibledataLength = DataSource.GetVisibledataLength();
            string path = Application.dataPath + "/pointList.csv";
            Debug.Log("save to:" + path);
            // Debug.Log("Export:" + visibledataLength + " " + dataLength);
            // Debug.Log("Target:" + target.transform.position.x + " " + target.transform.position.y + " " + target.transform.position.z);
            // Debug.Log("Export:" + visibledataLength + " " + dataLength);
            // Debug.Log("Target:" + target.transform.position.x + " " + target.transform.position.y + " " + target.transform.position.z);
            if (visibledataLength > 0)
            {
                // create file
                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                // create stream writer
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
                // write title
                sw.WriteLine("x,y,z,alpha,beta,gamma");
                // write data
                for (int i = 0; i < visibledataLength; i++)
                {
                    sw.WriteLine(pointList[i].x + "," + pointList[i].y + "," + pointList[i].z + "," + pointList[i].alpha + "," + pointList[i].beta + "," + pointList[i].gamma);
                }
                // clear buffer
                sw.Flush();
                // close stream
                sw.Close();
                fs.Close();
            }
        }

        // exit: set the game object as invisible
        public void showUp(int value)
        {
            // Debug.Log("showUp:" + value);
            if(value == 1)
            {
                trackPanel.SetActive(true);
            }
        }

        public void exit()
        {
            export();
            ClearPoint();
            trackPanel.SetActive(false);
            // toolbar is a dropdown component, please set it as the first option
            int itemIndexToDisplay = 0;
            toolbar.value = itemIndexToDisplay;
        }


        // ----------------------------- personal -----------------------------
        /*
        #region Testing
        private void OnDrawGizmos()
        {
            if (_recyclableScrollRect is VerticalRecyclingSystem)
            {
                ((VerticalRecyclingSystem)_recyclableScrollRect).OnDrawGizmos();
            }

            if (_recyclableScrollRect is HorizontalRecyclingSystem)
            {
                ((HorizontalRecyclingSystem)_recyclableScrollRect).OnDrawGizmos();
            }

        }
        #endregion
        */
    }
}