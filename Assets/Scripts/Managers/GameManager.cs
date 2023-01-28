using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseSingleton<GameManager>
{
    private int _levelIndex;
    [SerializeField] private GameObject[] _levels;
    private GameObject _lastLevel;

    void Start()
    {
        onInitialize();
    }

    private void onInitialize()
    {
        UIManager.instance.onInitialize();
        TutorialManager.instance.onInitialize();
        loadLevel();
    }

    private void loadLevel()
    {
        if (_lastLevel != null)
            DestroyImmediate(_lastLevel);

        UIManager.instance.hide();
        _lastLevel = Instantiate(_levels[_levelIndex % _levels.Length]);
    }
    public void nextLevel()
    {
        _levelIndex++;
        loadLevel();
    }
    public void retryLevel()
    {
        loadLevel();
    }
    public void prevLevel()
    {
        _levelIndex--;
        if (_levelIndex < 0)
            _levelIndex = 0;
        loadLevel();
    }

}
