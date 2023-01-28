using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : BaseSingleton<TutorialManager>
{
    [SerializeField] private GameObject _swerveTutorial;
    public void onInitialize()
    {
        _swerveTutorial.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _swerveTutorial.SetActive(false);
            this.enabled = false;
        }
    }
}
