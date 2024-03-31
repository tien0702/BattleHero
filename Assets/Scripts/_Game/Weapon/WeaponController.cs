using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class WeaponController : BaseEntity
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }

    protected override void OnLevelUp(int level)
    {

    }
}
