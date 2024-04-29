using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class NearestTarget : ICheckBestTarget
{
    public Transform Own;
    public bool CheckBestTarget(Transform current, Transform next)
    {
        float distance1 = Vector3.Distance(Own.position, current.position);
        float distance2 = Vector3.Distance(Own.position, next.position);

        return distance1 > distance2;
    }
}
