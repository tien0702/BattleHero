using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

public class TestDame : MonoBehaviour, IDamageable
{
    public void TakeDame(DamageMessage message)
    {
        var floatText = ServiceLocator.Current.Get<FloatingTextService>().GetByName("Heal");
        floatText.Spawn(transform.position, message.Dame);

        if (message.Effects != null)
        {
            foreach (var eff in message.Effects)
            {
                if (eff != null) { eff.HandleEffect(gameObject, message.Direction); }
            }
        }
    }

    public void Test()
    {

    }
}
