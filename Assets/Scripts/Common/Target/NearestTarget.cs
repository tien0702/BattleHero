using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class NearestTarget : MonoBehaviour, ICheckBestTarget
{
    public bool CheckBestTarget(Transform target1, Transform target2)
    {
        float distance1 = Vector3.Distance(transform.position, target1.position);
        float distance2 = Vector3.Distance(transform.position, target2.position);

        return distance1 < distance2;
    }
}
