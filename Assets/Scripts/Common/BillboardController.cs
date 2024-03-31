using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour
{
    public Transform CameraTarget;

    private void Awake()
    {
        if(CameraTarget == null) CameraTarget = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + CameraTarget.forward);
    }
}
