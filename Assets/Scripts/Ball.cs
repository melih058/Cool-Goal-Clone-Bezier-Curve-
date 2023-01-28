using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, BaseObstacleDelegate
{
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    private Vector3 _lastPosition;
    private Coroutine _moveCoroutine;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void shot(Vector3[] pathPositions)
    {
        _moveCoroutine = StartCoroutine(ballMoveRoutine(pathPositions));
    }
    private IEnumerator ballMoveRoutine(Vector3[] pathPositions)
    {
        for (int i = 0; i < pathPositions.Length; i++)
        {
            float duration = 0f;
            Vector3 targetPos = pathPositions[i];
            Vector3 initPos = transform.position;
            while (duration <= 1f)
            {
                transform.position = Vector3.Lerp(initPos, targetPos, duration);
                _velocity = transform.position - _lastPosition;
                _lastPosition = transform.position;
                duration += Time.deltaTime * 50;
                yield return null;
            }

        }

        setActiveRigidbody();
        _rigidbody.AddForce(0.5f * _velocity / Time.deltaTime, ForceMode.VelocityChange);
        StartCoroutine(failRoutine());

    }

    public void onBaseObstacleDelegate(IBaseObstacle baseObstacle)
    {
        StopCoroutine(_moveCoroutine);
        setActiveRigidbody();
        Vector3 forceDir = (transform.position - baseObstacle.obstacleTransform.position).normalized;
        _rigidbody.AddForce(forceDir * 3, ForceMode.Impulse);
        StartCoroutine(failRoutine());

    }

    private void setActiveRigidbody()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
    }

    private IEnumerator failRoutine()
    {
        yield return new WaitForSeconds(3f);
        UIManager.instance.showRetry();
    }
}
