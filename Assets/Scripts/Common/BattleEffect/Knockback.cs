using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

public class KnockbackInfo
{
    public float Force;
}

public class Knockback : IBattleEffect, IInfo
{
    public KnockbackInfo Info { get; private set; }
    public void HandleEffect(GameObject target, Vector3 direction)
    {
        var rb = target.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(direction.normalized * Info.Force, ForceMode.Impulse);
    }

    public void SetInfo(object info)
    {
        if (info is KnockbackInfo)
        {
            this.Info = (KnockbackInfo)info;
        }
    }
}
