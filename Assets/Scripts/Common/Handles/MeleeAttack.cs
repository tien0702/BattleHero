using UnityEngine;
using TT;
using System.Collections.Generic;

[System.Serializable]
public class MeleeAttackInfo
{
    public float Range;
    public string LayerMark;
    public string BattleEffect;
}

public class MeleeAttack : BaseHandle, IOwn, IInfo
{
    public MeleeAttackInfo Info { private set; get; }

    //References
    HeroController _hero;
    EntityStatController _statController;
    AnimEventReceiver _animationEvent;
    Collider _weapCollider;
    CameraShake _cameraShake;
    ObjectPool _effectPool;

    BattleEffect[] _battleEffects;

    public override void Handle()
    {
        _animationEvent.OnAttack += OnAttack;
        _animationEvent.OnEndAttack += OnEndAttack;
        EndHandle();
    }

    void OnAttack()
    {
        _weapCollider.enabled = true;
        _hero?.Weapon?.Events.RegisterEvent(WeaponController.WeapEvent.OnHitTarget, OnHitTarget);
    }

    void OnEndAttack()
    {
        _weapCollider.enabled = false;
        _hero?.Weapon?.Events.UnRegisterEvent(WeaponController.WeapEvent.OnHitTarget, OnHitTarget);
    }

    void OnHitTarget(object data)
    {
        Transform target = data as Transform;
        _cameraShake.Shake(0.1f, 0.05f);

        IDamageable[] damageables = target.GetComponents<IDamageable>();

        DamageMessage message = new DamageMessage()
        {
            Attacker = _hero.gameObject,
            Dame = (int)_statController.GetStatByID(DefineStatID.ATK).FinalValue,
            Direction = target.transform.position - _hero.transform.position,
            BattleEffects = _battleEffects
        };
        for(int i = 0; i < _battleEffects.Length; i++)
        {
            if(_battleEffects[i].Type == BattleEffectType.Knockback)
            {
                float force = _battleEffects[i].Params[0];
                _battleEffects[i].Params = new float[4];
                _battleEffects[i].Params[0] = force;
                _battleEffects[i].Params[1] = message.Direction.x;
                _battleEffects[i].Params[2] = message.Direction.y;
                _battleEffects[i].Params[3] = message.Direction.z;
            }
        }
        HashSet<Transform> targets = new HashSet<Transform>();
        for(int i = 0; i < damageables.Length; i++)
        {
            if (!targets.Add(target)) continue;
            damageables[i].TakeDame(message);
        }
    }

    public override void ResetHandle()
    {
        _animationEvent.OnAttack -= OnAttack;
        _animationEvent.OnEndAttack -= OnEndAttack;
    }

    public void SetInfo(object info)
    {
        if (info is MeleeAttackInfo)
        {
            this.Info = (MeleeAttackInfo)info;
            _battleEffects = BattleEffectController.Get(Info.BattleEffect);
            _cameraShake = GameObject.FindAnyObjectByType<CameraShake>();
        }
    }

    public void SetOwn(object own)
    {
        _hero = (own as StateController).transform.GetComponent<HeroController>();
        _statController = _hero.GetComponent<EntityStatController>();
        var obj = GameObjectUtilities.FindObjectByTag(_hero.transform, "Model");
        _animationEvent = obj.GetComponent<AnimEventReceiver>();
        _weapCollider = _hero.Weapon.GetComponent<Collider>();
        _effectPool = ServiceLocator.Current.Get<ObjectPool>();
    }
}
