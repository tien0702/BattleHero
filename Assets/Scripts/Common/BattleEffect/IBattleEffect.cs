using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleEffect
{
    void HandleEffect(GameObject target, Vector3 direction);
}
