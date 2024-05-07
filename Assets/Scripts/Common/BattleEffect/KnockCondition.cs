using System;
using TT;

public class KnockCondition : BaseCondition, IOwn
{
    BattleEffectController _battleEffect;
    public void SetOwn(object own)
    {
        StateController state = own as StateController;
        _battleEffect = state.GetComponent<BattleEffectController>();
        _battleEffect.Events.RegisterEvent(BattleEffectType.Knockback, OnHitKnock);
        _battleEffect.Events.RegisterEvent(BattleEffectType.Knockup, OnHitKnock);
        state.Events.RegisterEvent(StateController.StateEventType.OnExit,
        (StateController state) =>
        {
            this.SetSuitableCondition(false);
        });
    }

    void OnHitKnock(BattleEffect battleEffect)
    {
        this.SetSuitableCondition(true);
    }
}
