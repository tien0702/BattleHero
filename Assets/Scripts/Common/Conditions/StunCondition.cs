using TT;

public class StunCondition : BaseCondition, IOwn
{
    HeroController _heroCtrl;
    public void SetOwn(object own)
    {
        StateController state = own as StateController;
        _heroCtrl = state.GetComponent<HeroController>();
        _heroCtrl.Events.RegisterEvent(HeroController.HeroEvent.OnTakeDamage, OnTakeDamage);
    }

    void OnTakeDamage(object data)
    {
        DamageMessage message = data as DamageMessage;
        for(int i = 0; i < message.Effects.Length; i++)
        {
            /*if (message.Effects[i])
            {
                
            }*/
        }
    }
}
