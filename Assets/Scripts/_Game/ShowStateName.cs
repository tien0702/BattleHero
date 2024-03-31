using System.Collections;
using System.Collections.Generic;
using TMPro;
using TT;
using UnityEngine;

public class ShowStateName : MonoBehaviour
{
    public GameObject target;
    TextMeshProUGUI textMeshProUGUI;

    StateMachine stateMachine;

    private void Start()
    {
        target = GameObject.FindAnyObjectByType<PlayerController>().gameObject;
        stateMachine = target.GetComponent<StateMachine>();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        stateMachine.Events.RegisterEvent(StateMachine.StateMachineEventType.OnChangeState
            , (StateController state) =>
            {
                textMeshProUGUI.text = state.Info.StateName;
            });
    }
}
