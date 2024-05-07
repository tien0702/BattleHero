using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDisableParticle : DelayAction
{
    ParticleSystem _particle;

    private void Awake()
    {
        if (!TryGetComponent(out _particle))
        {
            Debug.Log("ParticleSystem is null!");
            return;
        }

        float duration = _particle.main.duration;
        this.SetDelay(duration);
    }

    private void OnEnable()
    {
        float duration = _particle.main.duration;
        this.SetDelay(duration);
    }

    private void OnDisable()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
    }

    protected override void OnDelayComplete(float delay)
    {
        gameObject.SetActive(false);
    }
}
