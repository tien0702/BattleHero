using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class HeroController : EntityController, IDamageable
{
    public HealthController HealCtrl { protected set; get; }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {

            var floatText = ServiceLocator.Current.Get<FloatingTextService>().GetByName("Heal");
            floatText.Spawn(transform.position, 100);
        }
    }
    public void TakeDame(DamageMessage message)
    {
        var floatText = ServiceLocator.Current.Get<FloatingTextService>().GetByName("Heal");
        floatText.Spawn(transform.position, message.Dame);
        if (HealCtrl != null) HealCtrl.CurrentValue -= message.Dame;
    }
}
