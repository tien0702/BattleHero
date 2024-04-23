using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class WeaponController : MonoBehaviour
{
    #region Events
    public enum WeapEvent { OnAttack, OnEndAttack, OnHitTarget }
    public ObserverEvents<WeapEvent, object> Events { protected set; get; }
    = new ObserverEvents<WeapEvent, object>();
    #endregion
    [SerializeField] Collider _weaponCollider;

    public HeroController Owner;

    protected virtual void OnTriggerEnter(Collider other)
    {
        Events.Notify(WeapEvent.OnHitTarget, other.transform.parent != null ? other.transform.parent : other.transform);
    }
}
