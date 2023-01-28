using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBezierPoint
{
    bool isStopped { get; }
    bool isCenter { get; }
    public void tryUpdateLerpValue(float value);
}

public class BezierPoint : MonoBehaviour, IBezierPoint
{
    private bool _isStopped;
    private bool _isCenter;
    private Vector3 _leftBound;
    private Vector3 _rightBound;
    private float _lerpValue;
    [SerializeField] private float _offset;

    public void onInitialize()
    {
        _leftBound = transform.localPosition - transform.right * _offset;
        _rightBound = transform.localPosition + transform.right * _offset;
        _lerpValue = 0.5f;
        tryUpdateLerpValue(0f);
    }


    public void tryUpdateLerpValue(float value)
    {
        _lerpValue += value;
        _lerpValue = Mathf.Clamp01(_lerpValue);
        checkStop();
        checkCenter();
        updatePosition();
    }

    private void updatePosition()
    {
        transform.localPosition = Vector3.Lerp(_leftBound, _rightBound, _lerpValue);
    }

    private void checkStop()
    {
        if (_lerpValue <= 0f || _lerpValue >= 1f)
        {
            _isStopped = true;
        }
        else
        {
            _isStopped = false;
        }
    }
    private void checkCenter()
    {
        if (Math.Round(Mathf.Abs(_lerpValue - 0.5f), 1) <= Mathf.Epsilon)
        {
            if (!_isCenter)
                _lerpValue = 0.5f;
            _isCenter = true;
        }
        else
        {
            _isCenter = false;
        }
    }


    public bool isStopped => _isStopped;

    public bool isCenter => _isCenter;
}
