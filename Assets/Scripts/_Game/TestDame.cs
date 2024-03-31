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
    }
}
