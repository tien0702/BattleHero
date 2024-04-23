using TT;

public class DeadCondition : BaseCondition, IOwn
{
    HeroController _hero;
    void OnTakeDamage(object data)
    {
        this.SetSuitableCondition(_hero.HealCtrl.CurrentValue <= 0);
    }

    void OnExitState(StateController state)
    {
        this.ResetCondition();
    }

    public void SetOwn(object own)
    {
        var state = (own as StateController);
        _hero = state.GetComponent<HeroController>();
        _hero.Events.RegisterEvent(HeroController.HeroEvent.OnTakeDamage, OnTakeDamage);
        state.Events.RegisterEvent(StateController.StateEventType.OnExit, OnExitState);
    }
}
