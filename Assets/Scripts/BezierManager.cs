using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierManager : MonoBehaviour
{
    [SerializeField] private int _numPoints;
    private Vector3[] _positions;
    [SerializeField] private Transform _p0;
    [SerializeField] private Transform _p1;
    [SerializeField] private Transform _p2;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _ball;
    [SerializeField] private Player _player;

    [SerializeField] private GameObject _bezierSphere;
    private Transform[] _bezierSpheres;
    private bool _didShot;
    public static BezierManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        onInitialize();
    }

    void Update()
    {
        if (_didShot)
            return;

        DrawQuadraticCurve();
    }

    private void OnDrawGizmos()
    {
        //int numPoints = (int)((_p0.position - _p1.position).magnitude + (_p1.position - _p2.position).magnitude);
        //Vector3[] testPositions = new Vector3[numPoints];

        //for (int i = 1; i < numPoints + 1; i++)
        //{
        //    float t = i / (float)numPoints;
        //    testPositions[i - 1] = CalcQuadraticBezierPoint(t, _p0.position, _p1.position, _p2.position);
        //}
        //Gizmos.color = Color.red;
        //for (int i = 0; i < testPositions.Length; i++)
        //{
        //    Gizmos.DrawSphere(testPositions[i], 0.1f);
        //}
    }
    private void onInitialize()
    {
        _positions = new Vector3[_numPoints];

        int bezierSphereCount = _numPoints / 2;
        _bezierSpheres = new Transform[bezierSphereCount];
        for (int i = 0; i < bezierSphereCount; i++)
        {
            _bezierSpheres[i] = Instantiate(_bezierSphere, transform).transform;
        }

        _p0.position = _ball.position + Vector3.up * 0.5f;

        Vector3 p2Pos = _target.position;
        p2Pos.y = _p0.position.y;
        _p2.position = p2Pos;
        p2Pos = _p2.localPosition;
        p2Pos.y = 0f;
        _p2.localPosition = p2Pos;

        Vector3 p1Pos = (_p2.position - _p0.position) / 2f;
        p1Pos.y = _p0.position.y;
        _p1.position = p1Pos;

        _p1.GetComponent<BezierPoint>().onInitialize();
        _p2.GetComponent<BezierPoint>().onInitialize();
    }
    private void DrawQuadraticCurve()
    {
        for (int i = 1; i < _numPoints + 1; i++)
        {
            float t = i / (float)_numPoints;
            _positions[i - 1] = CalcQuadraticBezierPoint(t, _p0.position, _p1.position, _p2.position);
        }
        setBezierSpheres();
    }

    private void setBezierSpheres()
    {
        for (int i = 0; i < _bezierSpheres.Length; i++)
        {
            _bezierSpheres[i].position = _positions[1 + i * 2];
        }

    }

    private Vector3 CalcQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }

    public void doShot()
    {
        _didShot = true;
        Vector3[] lastPathPositions = new Vector3[_bezierSpheres.Length];
        for (int i = 0; i < _bezierSpheres.Length; i++)
        {
            _bezierSpheres[i].gameObject.SetActive(false);
            lastPathPositions[i] = _bezierSpheres[i].position;
        }
        _player.doShot(lastPathPositions);
    }

}
