using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

[System.Serializable]
public class MeleeAttackInfo
{
    public float Range;
    public int LayerMark;
}

public class MeleeAttack : BaseHandle, IOwn, IInfo
{
    public MeleeAttackInfo Info { private set; get; }
    Transform obj;
    public override void Handle()
    {
        Collider[] colliders = Physics.OverlapSphere(obj.position, Info.Range, Info.LayerMark);
        foreach(Collider collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable.TakeDame(obj.gameObject, collider.transform.position - obj.position);
        }
    }

    public override void ResetHandle()
    {

    }

    public void SetInfo(object info)
    {
        throw new System.NotImplementedException();
    }

    public void SetOwn(object own)
    {
        throw new System.NotImplementedException();
    }
}
