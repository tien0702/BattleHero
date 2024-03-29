using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class FallCondition : BaseCondition, IOwn
{
    Rigidbody _rb;

    public void OnCheck(StateController state)
    {
        if (_rb.velocity.y < -0.1f)
        {
            this.SetSuitableCondition(true);
        }
        else
        {
            this.SetSuitableCondition(false);
        }
    }

    public void SetOwn(object own)
    {
        StateController state = own as StateController;
        _rb = state.GetComponentInChildren<Rigidbody>();
        state.Events.RegisterEvent(StateController.StateEventType.OnCheck, OnCheck);
    }
}
