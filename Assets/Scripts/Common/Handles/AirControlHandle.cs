using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class AirControlInfo
{
    public string StatID;
    public string JoystickID;
    public float RatioDecreased;
}

public class AirControlHandle : BaseHandleBehaviour, IInfo
{
    public AirControlInfo Info {  get; private set; }

    Rigidbody _rb;
    Stat _spd;
    JoystickController _joystick;

    private void Start()
    {
        StateController state = GetComponent<StateController>();
        EntityStatController statCtrl = state.GetComponent<EntityStatController>();
        _spd = statCtrl.GetStatByID(Info.StatID);
        _joystick = JoystickController.GetJoystick(Info.JoystickID);
        _rb = state.GetComponent<Rigidbody>();
        this.enabled = false;
    }

    private void Update()
    {
        if (_joystick.Direction.Equals(Vector3.zero)) return;
        Vector3 direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
        _rb.AddForce(direction.normalized * _spd.FinalValue * Info.RatioDecreased, ForceMode.Force);
        if (_rb.velocity.magnitude > _spd.FinalValue * Info.RatioDecreased)
        {
            _rb.velocity = direction.normalized * _spd.FinalValue;
        }
    }

    public override void Handle()
    {
        this.enabled = true;
        EndHandle();
    }

    public override void ResetHandle()
    {
        _rb.velocity = Vector3.zero;
        this.enabled = false;
    }

    public void SetInfo(object info)
    {
        if(info is AirControlInfo)
        {
            this.Info = (AirControlInfo)info;
        }
    }
}
