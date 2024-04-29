using UnityEngine;
using TT;

public class HeroController : EntityController, IDamageable
{
    #region Events
    public enum HeroEvent { OnTakeDamage }
    public ObserverEvents<HeroEvent, object> Events { protected set; get; }
    = new ObserverEvents<HeroEvent, object>();

    #endregion
    [SerializeField] protected Transform _weapAttachment;
    public HealthController HealCtrl { protected set; get; }
    public WeaponController Weapon { protected set; get; }
    public EntityStatController EntityStatCtrl { protected set; get; }
    public Transform Model;

    protected virtual void Start()
    {
        /*var model = GameObjectUtilities.FindObjectByTag(transform, "Model");
        Weapon = model.GetComponentInChildren<WeaponController>();

        Weapon.Owner = this;*/

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
        Events.Notify(HeroEvent.OnTakeDamage, message);
    }
}
