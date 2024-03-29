using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;
using System;

[System.Serializable]
public class KeyInfo
{
    public string KeyName;
}

public class InputKeyCondition : BaseCondition, IInfo, IOwn
{
    public KeyInfo Info { get; private set; }

    KeyCode key;

    public void SetInfo(object info)
    {
        if(info is KeyInfo)
        {
            this.Info = (KeyInfo)info;
            Enum.TryParse(Info.KeyName, out key);
        }
    }

    public void SetOwn(object own)
    {
        StateController state = own as StateController;
        state.Events.RegisterEvent(StateController.StateEventType.OnCheck, OnCheck);
    }

    void OnCheck(StateController state)
    {
        this.SetSuitableCondition(Input.GetKeyDown(key));
    }
}
