using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroyParticle : DelayAction
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

    protected override void OnDelayComplete(float delay)
    {
        Destroy(gameObject);
    }
}
