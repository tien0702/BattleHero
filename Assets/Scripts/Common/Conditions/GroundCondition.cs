using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

[System.Serializable]
public class GroundConditionInfo
{
    public string LayerName;
    public string GroundName;
    public bool OnGround;
}

public class GroundCondition : BaseCondition, IInfo, IOwn
{
    public GroundConditionInfo Info { private set; get; }
    GameObject _groundCheck;
    [SerializeField] LayerMask _layer;

    public void SetInfo(object info)
    {
        if(info is  GroundConditionInfo)
        {
            this.Info = (GroundConditionInfo)info;
            _layer = LayerMask.GetMask(Info.LayerName);
        }
    }

    public void SetOwn(object own)
    {
        var state = own as StateController;
        _groundCheck = GameObjectUtilities.FindObjectByTag(state.transform, Info.GroundName);
        state.Events.RegisterEvent(StateController.StateEventType.OnCheck, OnCheck);
    }

    void OnCheck(StateController state)
    {
        bool onGround = Physics.Raycast(_groundCheck.transform.position, Vector3.down, out RaycastHit hit, 0.1f, _layer);
        this.SetSuitableCondition((Info.OnGround) ? (onGround) : (!onGround));
    }
}
