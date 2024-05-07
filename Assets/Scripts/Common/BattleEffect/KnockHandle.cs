using UnityEngine;
using TT;

public class KnockHandle : BaseHandle, IOwn
{
    BattleEffectController _battleEffectCtrl;

    BattleEffect _data;
    public override void Handle()
    {

    }

    public override void ResetHandle()
    {

    }

    public void SetOwn(object own)
    {
        StateController state = (StateController)own;
        _battleEffectCtrl = state.GetComponent<BattleEffectController>();
    }

    void OnHitKnock(BattleEffect battleEffect)
    {

    }
}
