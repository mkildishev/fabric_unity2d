using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chain {

    public Point[] points;
    public GameObject[] pointes;
    public GameObject[] lines;
    public bool isChild;
    public int N;
    public Chain parent;
    public Chain left;
    public Chain right;
    public bool iwashere;
    public bool isReversed;
    public double rad;



    public Chain(Point[] _points, GameObject[] _pointes, GameObject[] _lines,
        bool _isChild, int _N, Chain _parent = null, Chain _left= null, Chain _right = null)
    {
        points = _points;
        pointes = _pointes;
        lines = _lines;
        isChild = _isChild;
        N = _N;
        parent = _parent;
        left = _left;
        right = _right;
        iwashere = false;
        rad = 0;
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
