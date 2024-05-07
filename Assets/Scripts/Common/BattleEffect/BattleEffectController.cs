using SimpleJSON;
using System;
using System.Collections.Generic;
using TT;
using UnityEngine;

public enum BattleEffectType
{
    Knockback, Knockup, Stun
}

[System.Serializable]
public class BattleEffect
{
    public BattleEffectType Type;
    public float[] Params;
}

public class BattleEffectController : MonoBehaviour, IDamageable
{
    public static BattleEffect GetFromString(string data)
    {
        return JsonUtility.FromJson<BattleEffect>(data);
    }

    public static BattleEffect[] Get(string data)
    {
        BattleEffect[] result = null;
        JSONNode json = JSONObject.Parse(data);
        if (json.IsArray)
        {
            result = new BattleEffect[json.Count];
            for (int i = 0; i < json.Count; i++)
            {
                result[i] = GetFromString(json.AsArray[i].ToString());
            }
        }
        else
        {
            Debug.Log("Data is not array!");
        }

        return result;
    }

    #region Events
    public ObserverEvents<BattleEffectType, BattleEffect> Events { protected set; get; }
    = new ObserverEvents<BattleEffectType, BattleEffect>();
    #endregion

    Dictionary<BattleEffectType, Action<float[]>> _battleEffects = new Dictionary<BattleEffectType, Action<float[]>>();

    Rigidbody _rb;

    private void Awake()
    {
        _battleEffects.Add(BattleEffectType.Knockback, Knockback);
        _battleEffects.Add(BattleEffectType.Knockup, Knockup);
        _battleEffects.Add(BattleEffectType.Stun, Stun);

        _rb = GetComponent<Rigidbody>();
    }

    public void TakeDame(DamageMessage message)
    {
        for (int i = 0; i < message.BattleEffects.Length; i++)
        {
            BattleEffect effect = message.BattleEffects[i];
            Events.Notify(effect.Type, effect);
            _battleEffects[effect.Type].Invoke(effect.Params);
        }
    }

    void Knockback(float[] paramesters)
    {
        if (paramesters.Length < 4)
        {
            Debug.Log("Knockback not enough data");
            return;
        }
        float force = paramesters[0];
        Vector3 direction = new Vector3(paramesters[1], paramesters[2], paramesters[3]);
        _rb.AddForce(direction * force, ForceMode.Impulse);
    }

    void Knockup(float[] paramesters)
    {
        if (paramesters.Length < 1)
        {
            Debug.Log("Knockup not enough data");
            return;
        }
        float force = paramesters[0];
        _rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    void Stun(float[] paramesters)
    {

    }
}
