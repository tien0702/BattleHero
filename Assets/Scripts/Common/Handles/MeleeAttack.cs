using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

[System.Serializable]
public class MeleeAttackInfo
{
    public float Range;
    public int MaxTargets;
    public string LayerMark;
}

public class MeleeAttack : BaseHandle, IOwn, IInfo
{
    public MeleeAttackInfo Info { private set; get; }
    Transform obj;
    EntityStatController statController;
    public override void Handle()
    {
        Collider[] colliders = Physics.OverlapSphere(obj.position, Info.Range, LayerMask.GetMask(Info.LayerMark));

        for(int i = 0; (i < colliders.Length && i < Info.MaxTargets); i++) 
        {
            IDamageable damageable = colliders[i].GetComponent<IDamageable>();
            DamageMessage message = new DamageMessage()
            {
                Attacker = obj.gameObject,
                Dame = (int)statController.GetStatByID("ATK").FinalValue
            };

            damageable.TakeDame(message);
        }

        EndHandle();
    }

    public override void ResetHandle()
    {

    }

    public void SetInfo(object info)
    {
        if(info is MeleeAttackInfo)
        {
            this.Info = (MeleeAttackInfo)info;
        }
    }

    public void SetOwn(object own)
    {
        obj = (own as StateController).transform;
        statController = obj.GetComponent<EntityStatController>();
    }
}
