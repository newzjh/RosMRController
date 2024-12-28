using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;

/// <summary>
/// Demo controller class for Recyclable Scroll Rect. 
/// A controller class is responsible for providing the scroll rect with datasource. Any class can be a controller class. 
/// The only requirement is to inherit from IRecyclableScrollRectDataSource and implement the interface methods
/// </summary>

//Dummy Data model for demostraion
// public struct ContactInfo
// {
//     public string Name;
//     public string Gender;
//     public string id;
// }

// public struct Point
// {
//     public float x;
//     public float y;
//     public float z;
//     public float alpha;
//     public float beta;
//     public float gamma;
// }

public class PointTrack : MonoBehaviour, IRecyclableScrollRectDataSource
{

    [SerializeField]
    private RecyclableScrollRect _recyclableScrollRect;

    [SerializeField]
    private int _dataLength;

    // [SerializeField]
    public int _visibledataLength;

    // ----------------------------- personal -----------------------------
    // public Button AddButton;
    // public Button RemoveButton;
    // public Button ClearButton;
    public GameObject target;


    //Dummy data List
    // private List<ContactInfo> _contactList = new List<ContactInfo>();
    private List<Point> _pointList = new List<Point>();
    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        InitData();
        _recyclableScrollRect.DataSource = this;
    }

    //Initialising _contactList with dummy data 
    private void InitData()
    {
        if (_pointList != null) _pointList.Clear();

        // string[] genders = { "Male", "Female" };
        for (int i = 0; i < _dataLength; i++)
        {
            Point obj = new Point();
            // initiate every dim as null for each point
            obj.x = 0;
            obj.y = 0;
            obj.z = 0;
            obj.alpha = 0;
            obj.beta = 0;
            obj.gamma = 0;
            _pointList.Add(obj);
        }
        _visibledataLength = 0;
    }

    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        // return _pointList.Count;
        return _visibledataLength;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as PointCell;
        item.ConfigureCell(_pointList[index], index);
    }

    public void UpdatePointList(List<Point> newpointList, int length)
    {
        Debug.Log("AddPoint given to datasource");
        _pointList = newpointList;
        _visibledataLength = length;
    }

    public List<Point> GetPointList()
    {
        return _pointList;
    }

    public int GetVisibledataLength()
    {
        return _visibledataLength;
    }

    public int GetdataLength()
    {
        return _dataLength;
    }

    public void setVisibledatalength(int length)
    {
        _visibledataLength = length;
    }

    #endregion
}