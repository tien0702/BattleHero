using UnityEngine;
using TT;

public class HeroController : EntityController, IDamageable
{
    #region Events
    public enum HeroEvent { OnTakeDamage }
    public ObserverEvents<HeroEvent, object> Events { protected set; get; }
    = new ObserverEvents<HeroEvent, object>();

    #endregion

    #region References
    [Header("References")]
    [SerializeField] protected Transform _weapAttachment;
    public Transform Model;
    #endregion

    public HealthController HealCtrl { protected set; get; }
    public WeaponController Weapon { protected set; get; }
    public EntityStatController EntityStatCtrl { protected set; get; }

    protected virtual void Start()
    {
        EntityStatCtrl = GetComponent<EntityStatController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var floatText = ServiceLocator.Current.Get<FloatingTextService>().GetByName("Heal");
            floatText.Spawn(transform.position, 100);
        }
    }

    public void TakeDame(DamageMessage message)
    {
        var floatText = ServiceLocator.Current.Get<FloatingTextService>().GetByName("Heal");
        floatText.Spawn(transform.position, message.Dame);
        if (HealCtrl != null) HealCtrl.CurrentValue -= message.Dame;

        // effect

        var _effectPool = ServiceLocator.Current.Get<ObjectPool>();

        var ef = _effectPool.GetObject<ParticleSystem>("HitEffect");
        ef.transform.position = transform.position + Vector3.up;
        ef.Play();
        LeanTween.delayedCall(ef.main.duration, () => { ef.gameObject.SetActive(false); });

        Events.Notify(HeroEvent.OnTakeDamage, message);
    }
}
