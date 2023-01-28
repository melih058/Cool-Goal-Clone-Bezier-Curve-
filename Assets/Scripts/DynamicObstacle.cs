using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacle : BaseObstacle
{
    [SerializeField] private float _moveOffset;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    private Vector3 _leftPos;
    private Vector3 _rightPos;
    void Start()
    {
        _rightPos = transform.position + transform.right * _moveOffset;
        _leftPos = transform.position - transform.right * _moveOffset;
        transform.position = _leftPos;
        StartCoroutine(moveRoutine());
    }

    private IEnumerator moveRoutine()
    {
        _animator.CrossFadeInFixedTime("SideStep_L", 0.3f);
        float duration = 0f;
        Vector3 initPos = transform.position;
        while (duration <= 1f)
        {
            transform.position = Vector3.Lerp(initPos, _rightPos, duration);
            duration += Time.deltaTime * _moveSpeed;
            yield return null;
        }


        _animator.CrossFadeInFixedTime("SideStep_R", 0.3f);
        duration = 0f;
        initPos = transform.position;
        while (duration <= 1f)
        {
            transform.position = Vector3.Lerp(initPos, _leftPos, duration);
            duration += Time.deltaTime * _moveSpeed;
            yield return null;
        }
        StartCoroutine(moveRoutine());

    }



}
