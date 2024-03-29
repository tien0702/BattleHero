using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class WeaponController : BaseEntity
{
    public int Dame;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        var idm = other.gameObject.GetComponentInChildren<IDamageable>();
        if (idm != null)
        {
            idm.TakeDame(null, Vector3.zero);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }

    protected override void OnLevelUp(int level)
    {

    }
}
