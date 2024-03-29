using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;
using Unity.VisualScripting;

[System.Serializable]
public class SingleAttackInfo
{

}

public class SingleTarget : MonoBehaviour, IInfo, IOwn
{
    WeaponController _weapon;
    Collider _collider;

    public void SetInfo(object info)
    {

    }

    public void SetOwn(object own)
    {
        StateController state = own as StateController;
        _weapon = state.GetComponent<WeaponController>();

        _collider = _weapon.GetComponentInChildren<BoxCollider>();
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable idamage = other.gameObject.GetComponentInChildren<IDamageable>();
        if (idamage != null)
        {
            _collider.enabled = false;

            idamage.TakeDame(null, Vector3.zero);
        }
    }
}
