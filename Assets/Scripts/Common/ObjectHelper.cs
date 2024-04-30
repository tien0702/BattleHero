using System;
using System.Collections;
using TT;
using UnityEngine;

public class ObjectHelper : SingletonBehaviour<ObjectHelper>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void WaitOneFrame(Action callback)
    {
        StartCoroutine(Wait(callback));
    }

    IEnumerator Wait(Action callback)
    {
        yield return new WaitForEndOfFrame();
        callback?.Invoke();
    }
}
