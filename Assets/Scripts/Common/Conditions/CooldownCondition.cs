using System;
using TT;
using UnityEngine;

[System.Serializable]
public class CooldownInfo
{
    public float CooldownTime;
}

public class CooldownCondition : BaseCondition, IInfo
{
    [SerializeField] CooldownInfo _info;

    public void SetInfo(object data)
    {
        if (data is CooldownInfo)
        {
            _info = (CooldownInfo)data;
            this.SetSuitableCondition(true);
        }
    }

    public override void ResetCondition()
    {
        this.SetSuitableCondition(false);
        LeanTween.delayedCall(_info.CooldownTime, () => {
            this.SetSuitableCondition(true);
        });
    }
}