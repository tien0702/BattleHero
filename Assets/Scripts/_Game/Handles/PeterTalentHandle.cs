using UnityEngine;
using TT;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class PeterTalentInfo
{
    public float Radius;
    public float Multiplier;
    public string LayerMark;
    public string EffectsData;
}

public class PeterTalentHandle : BaseHandleBehaviour, IInfo
{
    public PeterTalentInfo Info { private set; get; }

    //References
    EntityStatController _statCtrl;
    AnimEventReceiver _animReceiver;
    CameraShake _cameraShake;
    ObjectPool _effectPool;

    //Informations
    IBattleEffect[] _effects;
    LayerMask _layerMask;

    private void Start()
    {
        _animReceiver = GetComponentInChildren<AnimEventReceiver>();
        _statCtrl = GetComponent<EntityStatController>();
        _effectPool = ServiceLocator.Current.Get<ObjectPool>();
    }

    public override void Handle()
    {
        _animReceiver.OnTriggerEvents += OnAnimEvent;
    }

    public override void ResetHandle()
    {
        _animReceiver.OnTriggerEvents -= OnAnimEvent;
    }

    public void OnAnimEvent(string evName)
    {
        if (!evName.Equals("Grounding")) return;
        EndHandle();
        _cameraShake.Shake(0.12f, 0.1f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, Info.Radius, _layerMask);
        
        // Create DamageMessage
        DamageMessage message = new DamageMessage()
        {
            Attacker = gameObject,
            Dame = (int)(_statCtrl.GetStatByID(DefineStatID.ATK).FinalValue * Info.Multiplier),
            Effects = _effects
        };

        for(int i = 0; i < colliders.Length; i++)
        {
            Transform target = colliders[i].transform.parent;
            if (target == null) continue;
            if(target.Equals(transform)) continue;

            if (target.TryGetComponent<IDamageable>(out var damageable))
            {
                var ef = _effectPool.GetObject<ParticleSystem>("HitEffect");
                ef.transform.position = target.transform.position + Vector3.up;
                ef.Play();
                LeanTween.delayedCall(ef.main.duration, () => { ef.gameObject.SetActive(false); });
                message.Direction = target.transform.position - transform.position;
                damageable.TakeDame(message);
            }
        }
    }

    public void SetInfo(object info)
    {
        if (info is PeterTalentInfo)
        {
            this.Info = (PeterTalentInfo)info;
            _effects = BattleEffectUtils.GetEffectsText(Info.EffectsData);
            _cameraShake = GameObject.FindAnyObjectByType<CameraShake>();
            _layerMask = LayerMask.GetMask(Info.LayerMark);
        }
    }
}
