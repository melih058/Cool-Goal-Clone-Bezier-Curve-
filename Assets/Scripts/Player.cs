using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Ball _ball;

    public void doShot(Vector3[] pathPositions)
    {
        StartCoroutine(shotRoutine(pathPositions));
    }
    private IEnumerator shotRoutine(Vector3[] pathPositions)
    {
        _animator.CrossFadeInFixedTime("Kick", 0.1f);
        yield return new WaitForSeconds(0.7f);
        _ball.shot(pathPositions);
        _animator.CrossFadeInFixedTime("Idle", 1f);

    }
}
