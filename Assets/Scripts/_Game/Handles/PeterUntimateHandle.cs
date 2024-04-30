using System.Collections.Generic;
using TT;
using UnityEngine;


[System.Serializable]
public class PeterUntimateInfo
{
    public string LayerMark;
    public string EffectsData;
}

public class PeterUntimateHandle : BaseHandleBehaviour, IOwn, IInfo
{
    public PeterUntimateInfo Info { private set; get; }
    //References
    HeroController _hero;
    EntityStatController _statController;
    AnimEventReceiver _animationEvent;
    Collider _weapCollider;
    CameraShake _cameraShake;
    JoystickController _joyMove;

    //Informations
    IBattleEffect[] _effects;

    public override void Handle()
    {
        _animationEvent.OnAttack += OnAttack;
        _animationEvent.OnEndAttack += OnEndAttack;
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
        EndHandle();
    }

    void OnHitTarget(object data)
    {
        Transform target = data as Transform;
        IDamageable damageable = target.GetComponent<IDamageable>();
        _cameraShake.Shake(0.1f, 0.05f);

        HashSet<Transform> targets = new HashSet<Transform>();
        if (damageable != null && targets.Add(target))
        {
            var efPool = ServiceLocator.Current.Get<ObjectPool>();

            var ef = efPool.GetObject<ParticleSystem>("HitEffect");

            ef.transform.position = target.position + Vector3.up;
            ef.Play();
            LeanTween.delayedCall(ef.main.duration, () => { ef.gameObject.SetActive(false); });
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
        _animationEvent.OnAttack -= OnAttack;
        _animationEvent.OnEndAttack -= OnEndAttack;
    }

    public void SetOwn(object own)
    {
        _hero = (own as StateController).transform.GetComponent<HeroController>();
        _statController = _hero.GetComponent<EntityStatController>();
        var obj = GameObjectUtilities.FindObjectByTag(_hero.transform, "Model");
        _animationEvent = obj.GetComponent<AnimEventReceiver>();
        _weapCollider = _hero.Weapon.GetComponent<Collider>();
        _joyMove = JoystickController.GetJoystick(GameManager.JoyMoveId);
    }

    public void SetInfo(object info)
    {
        if (info is PeterUntimateInfo)
        {
            this.Info = (PeterUntimateInfo)info;
            _effects = BattleEffectUtils.GetEffectsText(Info.EffectsData);
            _cameraShake = GameObject.FindAnyObjectByType<CameraShake>();
        }
    }
}
