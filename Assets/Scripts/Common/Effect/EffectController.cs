using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IEffect
{
    void ShowEffect(Action<IEffect> callbackOnComplete);
    void StopEffect();
}

public class EffectController : MonoBehaviour
{
    public Action Callback;
    protected IEffect[] _effects;
    protected int _countEffects;

    public void ShowEffects()
    {
        if(_effects == null || _effects.Count() == 0)
        {
            _effects = GetComponentsInChildren<IEffect>();
        }
        _countEffects = _effects.Length;
        foreach (IEffect effect in _effects)
        {
            effect.ShowEffect(OnEffectCompleted);
        }
    }

    public void StopAllEffects()
    {
        if( _effects == null ) { return; }
        foreach (IEffect effect in _effects)
        {
            effect.StopEffect();
        }
    }

    public void OnEffectCompleted(IEffect effect)
    {
        _countEffects -= 1;
        if (_countEffects <= 0 && Callback != null) Callback();
    }
}
