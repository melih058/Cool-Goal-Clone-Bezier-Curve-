using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>())
        {
            UIManager.instance.showRetry();
        }
    }
}
