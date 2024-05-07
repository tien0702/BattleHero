using TT;

public class StunCondition : BaseCondition, IOwn
{
    public void SetOwn(object own)
    {
        StateController state = own as StateController;
        BattleEffectController battleEffectCtrl = state.GetComponent<BattleEffectController>();
        battleEffectCtrl.Events.RegisterEvent(BattleEffectType.Stun,
        (BattleEffect ef) =>
        {
            this.SetSuitableCondition(true);
        });
        state.Events.RegisterEvent(StateController.StateEventType.OnExit,
        (StateController s) =>
        {
            SetSuitableCondition(false);
        });
    }
}
