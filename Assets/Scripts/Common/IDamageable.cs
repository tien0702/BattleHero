using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDame(GameObject attacker, Vector3 direction);
}
