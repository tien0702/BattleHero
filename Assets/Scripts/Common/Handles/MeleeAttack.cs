using UnityEngine;
using TT;
using System.Collections.Generic;

[System.Serializable]
public class MeleeAttackInfo
{
    public float Range;
    public int MaxTargets;
    public string LayerMark;
    public string EffectsData;
}

public class MeleeAttack : BaseHandle, IOwn, IInfo
{
    public MeleeAttackInfo Info { private set; get; }

    //References
    HeroController _hero;
    EntityStatController _statController;
    AnimationEventReceiver _animationEvent;
    Collider _weapCollider;

    //Informations
    IBattleEffect[] _effects;
    HashSet<Transform> _targets = new HashSet<Transform>();
    List<GameObject> _hitEffectSpawned = new List<GameObject>();

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
        IDamageable damageable = target.GetComponent<IDamageable>();

        if (damageable != null && _targets.Add(target))
        {
            var prefab = Resources.Load<ParticleSystem>("Prefabs/Effects/RoundHitRed");

            var ef = GameObject.Instantiate(prefab, target.transform);
            _hitEffectSpawned.Add(ef.gameObject);

            ef.Play();
            if (damageable != null)
            {
                DamageMessage message = new DamageMessage()
                {
                    Attacker = _hero.gameObject,
                    Dame = (int)_statController.GetStatByID(DefineStatID.ATK).FinalValue,
                    Direction = target.transform.position - _hero.transform.position,
                    Effects = _effects
                };

                damageable.TakeDame(message);
            }
        }
    }

    public override void ResetHandle()
    {
        //GameObject.Destroy(_effect.gameObject);
        _targets.Clear();
        _animationEvent.OnAttack -= OnAttack;
        _animationEvent.OnEndAttack -= OnEndAttack;
    }

    public void SetInfo(object info)
    {
        if (info is MeleeAttackInfo)
        {
            this.Info = (MeleeAttackInfo)info;
            _effects = BattleEffectUtils.GetEffectsText(Info.EffectsData);
        }
    }

    public void SetOwn(object own)
    {
        _hero = (own as StateController).transform.GetComponent<HeroController>();
        _statController = _hero.GetComponent<EntityStatController>();
        var obj = GameObjectUtilities.FindObjectByTag(_hero.transform, "Model");
        _animationEvent = obj.GetComponent<AnimationEventReceiver>();
        _weapCollider = _hero.Weapon.GetComponent<Collider>();
    }
}
