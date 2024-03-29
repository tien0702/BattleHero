using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

[System.Serializable]
public class MovementHandleInfo
{
    public string StatID;
    public string JoystickID;
}

public class MovementHandle : BaseHandleBehaviour, IInfo
{
    public MovementHandleInfo Info { get; private set; }
    Rigidbody _rb;
    Stat _spd;
    JoystickController _joystick;

    private void Start()
    {
        StateController state = GetComponent<StateController>();
        EntityStatController statCtrl = state.GetComponent<EntityStatController>();
        _spd = statCtrl.GetStatByID(Info.StatID);
        _joystick = JoystickController.GetJoystick(Info.JoystickID);
        _joystick.Events.RegisterEvent(JoystickController.JoystickEvent.JoyEndDrag, 
            (JoystickController joy) => { EndHandle(); });
        _rb = state.GetComponent<Rigidbody>();
        this.enabled = false;
    }

    private void Update()
    {
        Vector3 direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
        _rb.AddForce(direction.normalized * _spd.FinalValue, ForceMode.Force);
        if(_rb.velocity.magnitude > _spd.FinalValue)
        {
            _rb.velocity = direction.normalized * _spd.FinalValue;
        }
    }

    public override void Handle()
    {
        this.enabled = true;
    }

    public override void ResetHandle()
    {
        _rb.velocity = Vector3.zero;
        this.enabled = false;
    }

    public void SetInfo(object info)
    {
        if(info is MovementHandleInfo)
        {
            Info = (MovementHandleInfo)info;
        }
    }
}
