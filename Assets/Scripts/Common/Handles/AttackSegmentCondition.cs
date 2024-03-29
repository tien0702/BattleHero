using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

[System.Serializable]
public class AttackSegmentConditionInfo
{
    public string PreSegmentName;
    public float TimeAction;
}

public class AttackSegmentCondition : BaseCondition, IInfo, IOwn
{
    public AttackSegmentConditionInfo Info { private set; get; }
    private StateController _preSegment;
    Animator _animator;

    public void SetInfo(object info)
    {
        if (info is AttackSegmentConditionInfo)
        {
            this.Info = info as AttackSegmentConditionInfo;
            this.SetSuitableCondition(true);
        }
    }

    public void SetOwn(object own)
    {
        StateController state = own as StateController;
        state.Events.RegisterEvent(StateController.StateEventType.OnEnter, OnStateEnter);
        state.Events.RegisterEvent(StateController.StateEventType.OnExit, OnStateExit);
        
        StateMachine stateMachine = state.GetComponent<StateMachine>();
        _preSegment = stateMachine.GetStateByName(Info.PreSegmentName);
        _preSegment.Events.RegisterEvent((StateController.StateEventType.OnEnter), OnPreSegmentEnter);

        _animator = stateMachine.GetComponentInChildren<Animator>();
    }

    void OnStateEnter(StateController state)
    {
        this.SetSuitableCondition(false);
    }

    void OnStateExit(StateController state)
    {
        this.SetSuitableCondition(true);
    }

    void OnPreSegmentEnter(StateController state)
    {
        this.SetSuitableCondition(false);
        float animTime = _animator.GetCurrentAnimatorStateInfo(0).length;
        LeanTween.delayedCall(Info.TimeAction * animTime, () =>
        {
            this.SetSuitableCondition(true);
        });
    }
}