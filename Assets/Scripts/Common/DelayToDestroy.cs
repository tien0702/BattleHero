using System.Collections;
using UnityEngine;

public class DelayToDestroy : MonoBehaviour
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
        Destroy(gameObject);
    }
}
