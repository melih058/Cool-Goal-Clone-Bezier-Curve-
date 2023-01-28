using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseObstacle
{
    Transform obstacleTransform { get; }
}
public interface BaseObstacleDelegate
{
    void onBaseObstacleDelegate(IBaseObstacle baseObstacle);
}

public class BaseObstacle : MonoBehaviour, IBaseObstacle
{

    private void OnTriggerEnter(Collider other)
    {

        if(other.GetComponent<BaseObstacleDelegate>() is BaseObstacleDelegate baseObstacleDelegate)
        {
            baseObstacleDelegate.onBaseObstacleDelegate(this);
        }
    }

    public Transform obstacleTransform =>transform;

}
