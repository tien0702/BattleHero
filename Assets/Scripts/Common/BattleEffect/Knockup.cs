using TT;
using UnityEngine;

public class KnockupInfo
{
    public float Force;
}

public class Knockup : IBattleEffect, IInfo
{
    public KnockupInfo Info { get; private set; }
    public void HandleEffect(GameObject target, Vector3 direction)
    {
        Debug.Log("knockback");
        var rb = target.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * Info.Force, ForceMode.Impulse);
    }

    public void SetInfo(object info)
    {
        if (info is KnockupInfo)
        {
            this.Info = (KnockupInfo)info;
        }
    }
}
