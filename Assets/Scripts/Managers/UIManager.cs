using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseSingleton<UIManager>
{
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private GameObject _retryPanel;
    private bool _anyPanelShowed;
    public void onInitialize()
    {

    }

    public void showSuccess()
    {
        if (_anyPanelShowed)
            return;

        _anyPanelShowed = true;
        _successPanel.SetActive(true);
    }
    public void hide()
    {
        _anyPanelShowed = false;
        _successPanel.SetActive(false);
        _retryPanel.SetActive(false);
    }
    public void showRetry()
    {
        if (_anyPanelShowed)
            return;

        _anyPanelShowed = true;
        _retryPanel.SetActive(true);
    }
}
