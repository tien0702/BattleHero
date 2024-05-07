using TT;

public class StunHandle : BaseHandle, IOwn
{
    BattleEffectController _battleEfCtrl;
    BattleEffect _ef;
    public override void Handle()
    {
        float time = _ef.Params[0];
        LeanTween.delayedCall(time, this.EndHandle);
    }

    public void SetOwn(object own)
    {
        StateController state = (StateController)own;
        _battleEfCtrl = state.GetComponent<BattleEffectController>();
        _battleEfCtrl.Events.RegisterEvent(BattleEffectType.Stun, OnStun);
    }

    void OnStun(BattleEffect ef)
    {
        this._ef = ef;
    }
}
