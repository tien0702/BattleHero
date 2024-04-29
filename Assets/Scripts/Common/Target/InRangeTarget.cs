using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class InRangeTarget : ICheckTarget
{
    public Transform Own;
    public float Range;
    public bool CheckTarget(Transform target)
    {
        if (target.Equals(Own)) return false;

        return Vector3.Distance(target.transform.position, Own.position) <= Range;
    }
}
