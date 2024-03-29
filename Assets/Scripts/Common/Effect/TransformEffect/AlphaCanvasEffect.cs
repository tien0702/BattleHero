using System;
using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class AlphaCanvasEffect : TTMonoBehaviour, IEffect
{
    [SerializeField] float _toAlpha;
    [SerializeField] float _originAlpha;

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowEffect(Action<IEffect> callbackOnComplete)
    {
        _canvasGroup.alpha = _originAlpha;
        _id = LeanTween.alphaCanvas(_canvasGroup, _toAlpha, _time)
            .setEase(_leanTweenType)
            .setOnComplete(() => { callbackOnComplete(this); }).id;
    }
    public void StopEffect()
    {
        LeanTween.cancel(_id);
        _canvasGroup.alpha = _originAlpha;
    }
}
