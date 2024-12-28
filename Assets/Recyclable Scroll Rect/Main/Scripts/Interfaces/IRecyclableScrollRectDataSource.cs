//MIT License
//Copyright (c) 2020 Mohammed Iqubal Hussain
//Website : Polyandcode.com 

/// <summary>
/// Interface for creating DataSource
/// Recyclable Scroll Rect must be provided a Data source which must inherit from this.
/// </summary>

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PolyAndCode.UI
{
    public struct Point
    {
        public float x;
        public float y;
        public float z;
        public float alpha;
        public float beta;
        public float gamma;
    }
    public interface IRecyclableScrollRectDataSource
    {
        // ----------------------------- personal -----------------------------
        // public int _dataLength;
        // public int _visibledataLength;
        // public List<Point> _pointList;
        // ----------------------------- personal -----------------------------
        int GetItemCount();
        void SetCell(ICell cell, int index);
        List<Point> GetPointList();
        int GetVisibledataLength();
        int GetdataLength();
        void UpdatePointList(List<Point> newpointList, int length);
        void setVisibledatalength(int length);
    }
}
