using TT;
using static ButtonController;

[System.Serializable]
public class PlayerButtonConditionInfo
{
    public string ButtonID;
}

public class PlayerButtonCondition : BaseCondition, IInfo, IOwn
{
    public PlayerButtonConditionInfo Info { get; private set; }
    ButtonController _buttonCtrl;
    public void SetInfo(object info)
    {
        if (info is PlayerButtonConditionInfo)
        {
            Info = (PlayerButtonConditionInfo)info;
        }
    }

    void OnButtonChangeState(ButtonEvent ev)
    {
        this.SetSuitableCondition(ev == ButtonEvent.OnDown);
    }

    public void SetOwn(object own)
    {
        var playerBtns = ServiceLocator.Current.Get<PlayerButtonController>();
        _buttonCtrl = playerBtns.GetByID(Info.ButtonID);
        _buttonCtrl.Events.RegisterEvent(ButtonEvent.OnUp, OnButtonChangeState);
        _buttonCtrl.Events.RegisterEvent(ButtonEvent.OnDown, OnButtonChangeState);
    }
}
