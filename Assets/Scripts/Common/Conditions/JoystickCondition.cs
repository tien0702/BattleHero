using TT;
using UnityEngine;

[System.Serializable]
public class JoystickConditionInfo
{
    public string JoystickID;
    public bool IsControl;
}

public class JoystickCondition : BaseCondition, IOwn, IInfo
{
    public JoystickConditionInfo Info { private set; get; }
    JoystickController _joystick;

    public void SetInfo(object data)
    {
        if (data is JoystickConditionInfo)
        {
            Info = (JoystickConditionInfo)data;
        }
    }

    public void SetOwn(object own)
    {
        (own as StateController).Events.RegisterEvent(StateController.StateEventType.OnCheck, OnCheck);
    }

    public void OnCheck(StateController state)
    {
        if(_joystick == null) 
        {
            _joystick = JoystickController.GetJoystick(Info.JoystickID);
        }

        _joystick = JoystickController.GetJoystick(Info.JoystickID);
        if (!Info.IsControl)
        {
            this.SetSuitableCondition(_joystick.Direction.Equals(Vector2.zero));
        }
        else
        {
            this.SetSuitableCondition(!_joystick.Direction.Equals(Vector2.zero));
        }
    }
}