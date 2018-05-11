using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point {
    public float X{  get; set; }
    public float Y { get; set; }
    public bool isChild { get; set; }

    public Point(float _X, float _Y, bool _isChild)
    {
        X = _X;
        Y = _Y;
        isChild = _isChild;
    }
}
