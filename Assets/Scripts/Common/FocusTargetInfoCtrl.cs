using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

public class FocusTargetInfoCtrl : MonoBehaviour, IGameService
{
    [Header("References")]
    [SerializeField] HealthController _healthCtrl;

    public virtual void SetTarget(HeroController heroController)
    {

    }

    protected virtual void OnChangeValue(float val)
    {
        _healthCtrl.CurrentValue = val;
    }
}
