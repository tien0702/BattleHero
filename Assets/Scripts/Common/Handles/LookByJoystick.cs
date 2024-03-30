using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

[System.Serializable]
public class LookByJoystickInfo
{
    public string JoystickID;
    public float RotateSpeed;
}

public class LookByJoystick : BaseHandleBehaviour, IInfo
{
    public LookByJoystickInfo Info { private set; get; }

    Transform _model;
    JoystickController _joystick;

    private void Awake()
    {
        this.enabled = false;
    }

    private void Start()
    {
        _joystick = JoystickController.GetJoystick(Info.JoystickID);
        _joystick.Events.RegisterEvent(JoystickController.JoystickEvent.JoyEndDrag,
            (JoystickController joy) => { EndHandle(); });
        var model = GameObjectUtilities.FindObjectByTag(transform, "Model");
        if (model == null)
        {
            Debug.Log("Can't find 'Model'");
        }
        else
        {
            _model = model.transform;
        }
    }

    private void Update()
    {
        Vector3 direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);

        direction = Vector3.Lerp(direction, _model.forward, Time.deltaTime * Info.RotateSpeed);
        _model.forward = direction;
    }

    public override void Handle()
    {
        this.enabled = true;
        //EndHandle();
    }

    public override void ResetHandle()
    {
        this.enabled = false;
    }

    public void SetInfo(object info)
    {
        if (info is LookByJoystickInfo)
        {
            this.Info = (LookByJoystickInfo)info;
        }
    }
}
