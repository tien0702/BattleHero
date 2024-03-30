using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDame : MonoBehaviour, IDamageable
{
    public DamageNumberMesh damageMesh;

    public void TakeDame(GameObject attacker, Vector3 direction)
    {
        Debug.Log("TakeDame");
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            damageMesh.Spawn(transform.position + Vector3.up, UnityEngine.Random.Range(0, 100).ToString());
        }
    }

}
