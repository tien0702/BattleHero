using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAction : MonoBehaviour
{
    protected Coroutine _coroutine;
    public virtual void SetDelay(float delay)
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Cooldown(delay));
    }

    protected virtual IEnumerator Cooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (_coroutine != null) StopCoroutine(_coroutine);
        OnDelayComplete(duration);
    }

    protected virtual void OnDelayComplete(float delay)
    {

    }
}
