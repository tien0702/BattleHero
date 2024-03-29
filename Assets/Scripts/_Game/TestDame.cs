using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDame : MonoBehaviour, IDamageable
{
    public void TakeDame(GameObject attacker, Vector3 direction)
    {
        Debug.Log("TakeDame");
    }

}
