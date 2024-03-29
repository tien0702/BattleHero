using System;
using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;
public class ScaleEffect : TTMonoBehaviour, IEffect
{
    [SerializeField] Vector3 _toScale;
    Vector3 _originScale;

    public void ShowEffect(Action<IEffect> callbackOnComplete)
    {
        transform.localScale = _originScale;
        this.ScalceTo(_toScale, () => { callbackOnComplete(this); });
    }
    public void StopEffect()
    {

    }
}
