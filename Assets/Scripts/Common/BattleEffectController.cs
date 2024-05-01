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

public class BattleEffectController : MonoBehaviour
{
    public virtual void HitEffects(BattleEffect[] effects)
    {

    }
}

public class BaseEffect
{
    public virtual void HandleEffect(BattleEffect effect)
    {

    }
}

public class KnockbackEffect : BaseEffect
{
    public override void HandleEffect(BattleEffect effect)
    {
        float force = effect.Params[0];
    }
}
