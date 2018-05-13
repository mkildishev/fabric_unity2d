using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain {

    public Point[] points;
    public GameObject[] pointes;
    public GameObject[] lines;
    public bool isChild;
    public int N;
    
    public Chain(Point[] _points, GameObject[] _pointes, GameObject[] _lines,
        bool _isChild, int _N)
    {
        points = _points;
        pointes = _pointes;
        lines = _lines;
        isChild = _isChild;
        N = _N;
    }

    public Point[] reverse()
    {
        Point[] reversePoints = new Point[N];
        for (int i = 0; i < N; i++)
        {
            reversePoints[N - 1 - i] = points[i];
        }
        return reversePoints;
    }

}
