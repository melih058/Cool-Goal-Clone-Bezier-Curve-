using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierController : MonoBehaviour
{
    [SerializeField] private BezierPoint _p1;
    [SerializeField] private BezierPoint _p2;
    [SerializeField] private float _sensivity;
    private Vector3 _lastMousePosition;
    private int SCREEN_WIDTH;
    private bool _didShot;

    void Start()
    {
        onInitialize();
    }
    void Update()
    {
        if (_didShot)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            float diffX = Input.mousePosition.x - _lastMousePosition.x;
            diffX /= SCREEN_WIDTH;
            diffX *= _sensivity;

            if (!_p1.isStopped && _p2.isCenter)
            {
                _p1.tryUpdateLerpValue(-diffX);
            }
            else
            {
                _p2.tryUpdateLerpValue(diffX);
                if (_p2.isCenter)
                {
                    _p1.tryUpdateLerpValue(-diffX);
                }
            }

            _lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _didShot = true;
            BezierManager.instance.doShot();
            _lastMousePosition = Input.mousePosition;
        }
    }
    private void onInitialize()
    {
        SCREEN_WIDTH = Screen.width;
    }



}
