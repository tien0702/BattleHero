using System;
using UnityEngine;

public class AnimEventReceiver : MonoBehaviour
{
    public Action<string> OnTriggerEvents;
    public Action OnAttack, OnEndAttack;
    public void Attack()
    {
        OnAttack?.Invoke();
    }

    public void EndAttack()
    {
        OnEndAttack?.Invoke();
    }

    public void OnTriggerEvent(string eventName)
    {
        OnTriggerEvents?.Invoke(eventName);
    }
}
