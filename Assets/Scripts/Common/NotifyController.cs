using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;
using TMPro;
using System;

public class NotifyController : MonoBehaviour, IGameService
{
    [SerializeField] TextMeshProUGUI _message;
    EffectController _effectController;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (!ServiceLocator.Current.IsRegistered<NotifyController>())
        {
            ServiceLocator.Current.Register(this);
        }
        _effectController = GetComponent<EffectController>();
        gameObject.SetActive(false);
    }

    public void Show(string message)
    {
        if(_message == null) _message = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(true);
        _effectController.StopAllEffects();
        _effectController.ShowEffects();
        _effectController.Callback = OnShowCompleted;
        _message.text = message;
    }

    void OnShowCompleted()
    {
        gameObject.SetActive(false);
    }
}
