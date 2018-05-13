using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FABRIC : MonoBehaviour {
    [SerializeField]
    private GameObject line;


    [SerializeField]
    private GameObject circle;
    public float lineSizeX
    {
        get
        {
            return line.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        }
    }

    public float lineSizeY
    {
        get
        {
            return line.GetComponent<SpriteRenderer>().sprite.bounds.size.y; 
        }
    }

    public float circleSizeX
    {
        get
        {
            return circle.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        }
    }

    public float circleSizeY
    {
        get
        {
            return circle.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        }
    }
    GameObject[] lines = new GameObject[3];
    Point[] points = new Point[4];
    Point t = new Point(3.0f, 1.0f);
    GameObject[] pointes = new GameObject[4];
    

    // Для мультиаффектора
    Point[] pointsFirst = new Point[3];
    Point[] pointsSecond = new Point[3];
    Point[] pointsThird = new Point[3];

    GameObject[] linesFirst = new GameObject[2];
    GameObject[] linesSecond = new GameObject[2];
    GameObject[] linesThird = new GameObject[2];
    

    GameObject[] pointesFirst = new GameObject[3];
    GameObject[] pointesSecond = new GameObject[3];
    GameObject[] pointesThird = new GameObject[3];
    Chain first;
    Chain second;
    Chain third;


    // Use this for initialization
    void Start () {
        //createBase(ref points, ref pointes, ref lines,3);
        //createBase(ref pointsFirst, ref pointesFirst, ref linesFirst, 4);
        createBaseME(ref pointsFirst, ref pointsSecond, ref pointsThird,
            ref pointesFirst, ref pointesSecond, ref pointesThird,
            ref linesFirst, ref linesSecond, ref linesThird, 3, 3, 3);
        first = new Chain(pointsFirst, pointesFirst, linesFirst,  false, 3);
        second = new Chain(pointsSecond, pointesSecond, linesSecond, true, 3);
        third = new Chain(pointsThird, pointesThird, linesThird, true, 3);
    }

	// Update is called once per frame
	void Update () {
        isClicked(second);
        isClicked(third);
        WaitAndMoveToNewPos(first);
        WaitAndMoveToNewPos(second);
        WaitAndMoveToNewPos(third);
    }

    private void createBase(ref Point[] _points, ref GameObject[] _pointes, ref GameObject[] _lines, int N)
    {

        for (int x = 0; x < N - 1; x++)
        {
            GameObject newLine = Instantiate(line);
            newLine.transform.position = new Vector3(lineSizeX * x, 0, 0);
            GameObject newCircle = Instantiate(circle);
            newCircle.transform.position = new Vector3(lineSizeX * x, 0, 0);
            _points[x] = new Point(newCircle.transform.position.x, newCircle.transform.position.y);
            _pointes[x] = newCircle;
            _lines[x] = newLine;
        }
        GameObject newCircleL = Instantiate(circle);
        newCircleL.transform.position = new Vector3(lineSizeX * (N - 1), 0, 0);
        _points[N - 1] = new Point(newCircleL.transform.position.x, newCircleL.transform.position.y);
        _pointes[N - 1] = newCircleL;
    }

    private void isClicked(Chain chain)
    {
        
        if (Input.GetMouseButton(0))
        {
            Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (a.x <= new Vector3(chain.points[chain.N-1].X+1f, chain.points[chain.N -1].Y+1f).x && a.x >= new Vector3(chain.points[chain.N -1].X - 1f, chain.points[chain.N -1].Y - 1f).x &&
                a.y <= new Vector3(chain.points[chain.N -1].X + 1f, chain.points[chain.N -1].Y + 1f).y && a.y >= new Vector3(chain.points[chain.N -1].X - 1f, chain.points[chain.N -1].Y - 1f).y) // Для перетягивания
            { 
                t = new Point(a.x, a.y);
                //Debug.Log(t.X); Debug.Log(t.Y);
                newPosition(chain, t);
            }
        }
    }

    void WaitAndMoveToNewPos(Chain chain)
    {
        for (int i = 0; i < chain.N; i++)
        {
            chain.pointes[i].transform.position = new Vector3(chain.points[i].X, chain.points[i].Y);
        }
        for (int i = 0; i < chain.N - 1; i++)
        {
            chain.lines[i].transform.position = new Vector3(chain.points[i].X, chain.points[i].Y);
        }
        for (int i = 0; i < chain.N - 1; i++)
        {
            Vector3 a = chain.pointes[i + 1].transform.position - chain.lines[i].transform.position;
            a.Normalize();
            float rot = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;
            chain.lines[i].transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }
    }


    private void createBaseME(ref Point[] _firstPoints, ref Point[] _secondPoints, ref Point[] _thirdPoints,
        ref GameObject[] _firstPointes, ref GameObject[] _secondPointes, ref GameObject[] _thirdPointes,
        ref GameObject[] _firstLines, ref GameObject[] _secondLines, ref GameObject[] _thirdLines,
        int N1, int N2, int N3)
    {
        for (int x = 0; x < N1-1; x++)
        {
            GameObject newLine = Instantiate(line);
            newLine.transform.position = new Vector3(lineSizeX * x, 0, 0);
            GameObject newCircle = Instantiate(circle);
            newCircle.transform.position = new Vector3(lineSizeX * x, 0, 0);
            _firstPoints[x] = new Point(newCircle.transform.position.x, newCircle.transform.position.y);
            pointesFirst[x] = newCircle;
            _firstLines[x] = newLine;
        }
        GameObject newCircleL = Instantiate(circle);
        newCircleL.transform.position = new Vector3(lineSizeX * (N1 - 1), 0, 0);
        _firstPoints[N1 - 1] = new Point(newCircleL.transform.position.x, newCircleL.transform.position.y);
        _firstPointes[N1 - 1] = newCircleL;

        float add = lineSizeX * (N1 - 1);
        for (int x = 0; x < N2 - 1; x++)
        {
            GameObject newLineS = Instantiate(line);
            newLineS.transform.position = new Vector3(add, lineSizeX * x, 0);
            GameObject newCircleS = Instantiate(circle);
            newCircleS.transform.position = new Vector3(add, lineSizeX * x, 0);
            _secondPoints[x] = new Point(newCircleS.transform.position.x, newCircleS.transform.position.y);
            pointesSecond[x] = newCircleS;
            _secondLines[x] = newLineS;
        }
        GameObject newCircleL1 = Instantiate(circle);
        newCircleL1.transform.position = new Vector3(add, lineSizeX * (N2 - 1), 0);
        _secondPoints[N2 - 1] = new Point(newCircleL1.transform.position.x, newCircleL1.transform.position.y);
        _secondPointes[N2 - 1] = newCircleL1;

        for (int i = 0; i < N2 - 1; i++)
        {
            Vector3 a = _secondPointes[i + 1].transform.position - _secondLines[i].transform.position;
            a.Normalize();
            float rot = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;
            _secondLines[i].transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }

        for (int x = 0; x < N3 - 1; x++)
        {
            GameObject newLineT = Instantiate(line);
            newLineT.transform.position = new Vector3(add, -lineSizeX * x, 0);
            Debug.Log(lineSizeX);
            GameObject newCircleT = Instantiate(circle);
            newCircleT.transform.position = new Vector3(add, -lineSizeX * x, 0);
            _thirdPoints[x] = new Point(newCircleT.transform.position.x, newCircleT.transform.position.y);
            pointesThird[x] = newCircleT;
            _thirdLines[x] = newLineT;
        }
        GameObject newCircleL2 = Instantiate(circle);
        newCircleL2.transform.position = new Vector3(add, -lineSizeX * (N3 - 1), 0);
        _thirdPoints[N3 - 1] = new Point(newCircleL2.transform.position.x, newCircleL2.transform.position.y);
        _thirdPointes[N3 - 1] = newCircleL2;

        for (int i = 0; i < N3 - 1; i++)
        {
            Vector3 a = _thirdPointes[i + 1].transform.position - _thirdLines[i].transform.position;
            a.Normalize();
            float rot = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;
            _thirdLines[i].transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }

    }
    float rad = 2.92f; //радиус - длина до суб-базы
    private void newPosition(Chain chain, Point t)
    {
        float tolerance = 0.001f;
        int iterCount = 100;
        float sum_dist = 0;
        float[] d = new float[chain.N - 1];
        float[] r = new float[chain.N - 1];
        float[] la = new float[chain.N - 1];
        Point[] p = new Point[chain.N];
        for (int i = 0; i < chain.N; i++)
        {
            p[i] = chain.points[i];
        }
        for (int i = 0; i < chain.N - 1; i++)
        {
            float X = p[i + 1].X - p[i].X;
            float Y = p[i + 1].Y - p[i].Y;
            Vector3 vec = new Vector3(X, Y, 0);
            d[i] = vec.magnitude;
            sum_dist += d[i];
        }
        sum_dist += 10.0f; // Можно воткнуть для плавного движения
        float dist;
        float tX = p[0].X - t.X;
        float tY = p[0].Y - t.Y;
        Vector3 start_to_target = new Vector3(tX, tY, 0);
        dist = start_to_target.magnitude;

        if (dist > sum_dist)
        {
            Debug.Log("Don't reach");
            for (int i = 0; i < chain.N - 1; i++)
            {
                float t1X = t.X - p[i].X;
                float t1Y = t.Y - p[i].Y;
                Vector3 tempD = new Vector3(t1X, t1Y);
                r[i] = tempD.magnitude;
                la[i] = d[i] / r[i];
                p[i + 1].X = p[i].X * (1 - la[i]) + t.X * la[i];
                p[i + 1].Y = p[i].Y * (1 - la[i]) + t.Y * la[i];
                iterCount -= 1;
            }
        }
        else
        {
            Point b1 = new Point(0, 0);
            Point b = p[0];

            Vector3 pt = new Vector3(p[chain.N - 1].X - t.X, p[chain.N - 1].Y - t.Y);
            float difA = pt.magnitude;
            while ((difA > tolerance) && (iterCount != 0)) //Сравнение нормы или количества итераций
            {
                p[chain.N - 1] = t;
                for (int i = chain.N - 2; i >= 0; i--)
                {
                    Vector3 tempD = new Vector3(p[i + 1].X - p[i].X, p[i + 1].Y - p[i].Y);
                    r[i] = tempD.magnitude;
                    la[i] = d[i] / r[i];
                    p[i].X = p[i + 1].X * (1 - la[i]) + p[i].X * la[i];
                    p[i].Y = p[i + 1].Y * (1 - la[i]) + p[i].Y * la[i];
                }
                if (chain.isChild)
                {
                    float difX = p[0].X - b1.X;
                    float difY = p[0].Y - b1.Y;
                    Vector3 vec = new Vector3(difX, difY, 0);
                    float temp_rad = vec.magnitude;
                    Point temp = p[0];
                    if (temp_rad > rad)// Вот тут временный костыль, который можно пофиксить геометрией
                    {//

                        while (temp_rad > rad) // Вот это всё надо, чтобы аффектор мог двигаться с некоторым запасом от центра.
                        {
                            float tempdifX = temp.X > 0 ? -0.05f : 0.05f;
                            float tempdifY = temp.Y > 0 ? -0.05f : 0.05f;
                            temp.X = temp.X + tempdifX; //whoitare
                            temp.Y = temp.Y + tempdifY;
                            difX = temp.X - b1.X;
                            difY = temp.Y - b1.Y;
                            vec = new Vector3(difX, difY, 0);
                            temp_rad = vec.magnitude;
                        }
                    }
                    p[0] = temp;//Тут можно ставить новую точку суб-базы
                }
                else
                    p[0] = b;
                for (int i = 0; i < chain.N - 1; i++)
                {
                    Vector3 tempD = new Vector3(p[i + 1].X - p[i].X, p[i + 1].Y - p[i].Y);
                    r[i] = tempD.magnitude;
                    la[i] = d[i] / r[i];
                    p[i + 1].X = p[i].X * (1 - la[i]) + p[i + 1].X * la[i];
                    p[i + 1].Y = p[i].Y * (1 - la[i]) + p[i + 1].Y * la[i];
                }
                pt = new Vector3(p[chain.N - 1].X - t.X, p[chain.N - 1].Y - t.Y);
                difA = pt.magnitude;
                iterCount -= 1;
                if (chain.isChild)
                {
                    newPosition(first, p[0]);
                    Point[] thirdPointReversed = third.reverse();
                    Point[] secondPointReversed = second.reverse();
                    

                    //pointsThird[0] = p[0];
                    for (int i = 0; i < chain.N - 1; i++)
                    {
                        Vector3 tempointsThirdD = new Vector3(pointsThird[i + 1].X - pointsThird[i].X, pointsThird[i + 1].Y - pointsThird[i].Y);
                        r[i] = tempointsThirdD.magnitude;
                        la[i] = d[i] / r[i];
                        pointsThird[i + 1].X = pointsThird[i].X * (1 - la[i]) + pointsThird[i + 1].X * la[i];
                        pointsThird[i + 1].Y = pointsThird[i].Y * (1 - la[i]) + pointsThird[i + 1].Y * la[i];
                    }
                }
            }
        }
        for (int i = 0; i < chain.N; i++)
        {
            chain.points[i] = p[i];
        }
    }

    private void newPositionOneEffector(Chain chain, Point t)
    {
        float tolerance = 0.001f;
        int iterCount = 100;
        float sum_dist = 0;
        float[] d = new float[chain.N - 1];
        float[] r = new float[chain.N - 1];
        float[] la = new float[chain.N - 1];
        Point[] p = new Point[chain.N];
        for (int i = 0; i < chain.N; i++)
        {
            p[i] = chain.points[i];
        }
        for (int i = 0; i < chain.N - 1; i++)
        {
            float X = p[i + 1].X - p[i].X;
            float Y = p[i + 1].Y - p[i].Y;
            Vector3 vec = new Vector3(X, Y, 0);
            d[i] = vec.magnitude;
            sum_dist += d[i];
        }
        float dist;
        float tX = p[0].X - t.X;
        float tY = p[0].Y - t.Y;
        Vector3 start_to_target = new Vector3(tX, tY, 0);
        dist = start_to_target.magnitude;

        if (dist > sum_dist)
        {
            Debug.Log("Don't reach");
            for (int i = 0; i < chain.N - 1; i++)
            {
                float t1X = t.X - p[i].X;
                float t1Y = t.Y - p[i].Y;
                Vector3 tempD = new Vector3(t1X, t1Y);
                r[i] = tempD.magnitude;
                la[i] = d[i] / r[i];
                p[i + 1].X = p[i].X * (1 - la[i]) + t.X * la[i];
                p[i + 1].Y = p[i].Y * (1 - la[i]) + t.Y * la[i];
                iterCount -= 1;
            }
        }
        else
        {
            Point b = chain.points[0];
            Vector3 pt = new Vector3(p[chain.N - 1].X - t.X, p[chain.N - 1].Y - t.Y);
            float difA = pt.magnitude;
            while ((difA > tolerance) && (iterCount != 0)) //Сравнение нормы или количества итераций
            {
                p[chain.N - 1] = t;
                for (int i = chain.N - 2; i >= 0; i--)
                {
                    Vector3 tempD = new Vector3(p[i + 1].X - p[i].X, p[i + 1].Y - p[i].Y);
                    r[i] = tempD.magnitude;
                    la[i] = d[i] / r[i];
                    p[i].X = p[i + 1].X * (1 - la[i]) + p[i].X * la[i];
                    p[i].Y = p[i + 1].Y * (1 - la[i]) + p[i].Y * la[i];
                }
                p[0] = b;
                for (int i = 0; i < chain.N - 1; i++)
                {
                    Vector3 tempD = new Vector3(p[i + 1].X - p[i].X, p[i + 1].Y - p[i].Y);
                    r[i] = tempD.magnitude;
                    la[i] = d[i] / r[i];
                    p[i + 1].X = p[i].X * (1 - la[i]) + p[i + 1].X * la[i];
                    p[i + 1].Y = p[i].Y * (1 - la[i]) + p[i + 1].Y * la[i];
                }
                pt = new Vector3(p[chain.N - 1].X - t.X, p[chain.N - 1].Y - t.Y);
                difA = pt.magnitude;
                iterCount -= 1;
            }
        }
        for (int i = 0; i < chain.N; i++)
        {
            chain.points[i] = p[i];
        }
    }


    /// Как вариант, отдельно оформить функции пересчета вперёд и назад

}
