using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

public class TestDame : MonoBehaviour
{
    private void Awake()
    {
        string data = "{\"Type\": \"Knockback\", \"Params\": [4]}";
        var s = BattleEffectController.GetFromString(data);
    }
}
