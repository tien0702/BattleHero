using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class JumpHandleInfo
{
    public string JoystickID;
    public float Force;
}

public class JumpHandle : BaseHandle, IInfo, IOwn
{
    public JumpHandleInfo Info { get; private set; }
    Rigidbody _rb;
    JoystickController _joystick;

    public override void Handle()
    {
        _rb.velocity = Vector3.zero;
        Vector3 direction = new Vector3(_joystick.Direction.x, 1, _joystick.Direction.y);
        _rb.AddForce(direction.normalized / 2f * Info.Force, ForceMode.Impulse);
        EndHandle();
    }

    public override void ResetHandle()
    {

    }

    public void SetInfo(object info)
    {
        if (info is JumpHandleInfo)
        {
            this.Info = (JumpHandleInfo)info;
        }
    }

    public void SetOwn(object own)
    {
        StateController state = own as StateController;
        _rb = state.GetComponent<Rigidbody>();
        _joystick = JoystickController.GetJoystick(Info.JoystickID);
    }
}
