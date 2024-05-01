using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class StunInfo
{
    public float Duration;
}

public class Stun : IBattleEffect, IInfo
{
    public StunInfo Info { get; private set; }
    public void HandleEffect(GameObject target, Vector3 direction)
    {
        EntityStatController statCtrl = target.GetComponent<EntityStatController>();
        Stat resStun = statCtrl.GetStatByID(DefineStatID.ResStun);
        float duration = Info.Duration - ((resStun.FinalValue * 0.01f) * Info.Duration);
    }

    public void SetInfo(object info)
    {
        if (info is StunInfo)
            Info = (StunInfo)info;
    }
}
