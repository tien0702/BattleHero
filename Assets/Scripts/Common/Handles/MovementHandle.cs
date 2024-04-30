using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

[System.Serializable]
public class MovementHandleInfo
{
    public float Multiplier;
    public bool EndImmediate;
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
        _spd = statCtrl.GetStatByID(DefineStatID.SPD);
        _joystick = JoystickController.GetJoystick(GameManager.JoyMoveId);
        _joystick.Events.RegisterEvent(JoystickController.JoystickEvent.JoyEndDrag,
            (JoystickController joy) => { EndHandle(); });
        _rb = state.GetComponent<Rigidbody>();
        this.enabled = false;
    }

    private void Update()
    {
        if (!Info.EndImmediate && !_joystick.IsControl)
        {
            EndHandle();
            return;
        }

        Vector3 direction = _joystick.Direction3D;
        float targetSpeed = _spd.FinalValue * Info.Multiplier;
        _rb.AddForce(direction.normalized * targetSpeed, ForceMode.Force);
        if (_rb.velocity.magnitude > targetSpeed)
        {
            _rb.velocity = direction.normalized * targetSpeed;
        }
    }

    public override void Handle()
    {
        this.enabled = true;
        if (Info.EndImmediate) EndHandle();
    }

    public override void ResetHandle()
    {
        _rb.velocity = Vector3.zero;
        this.enabled = false;
    }

    public void SetInfo(object info)
    {
        if (info is MovementHandleInfo)
        {
            Info = (MovementHandleInfo)info;
        }
    }
}
