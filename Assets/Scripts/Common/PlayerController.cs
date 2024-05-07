using TT;
using UnityEngine;

public class PlayerController : HeroController
{
    public void SetInfo(EntityInfo info)
    {
        this.Info = info;
        this.Level = Info.Level;
    }

    protected override void Start()
    {
        base.Start();
        var healthBar = Resources.Load<HealthController>("Prefabs/UI/HeroHealthBar");
        var groundDirection = Resources.Load<GroundDirectionController>("Prefabs/GroundDirection");
        var gd = Instantiate(groundDirection);
        gd.Model = GameObjectUtilities.FindObjectByTag(transform, "Model").transform;
        gd.GroundCheck = GameObjectUtilities.FindObjectByTag(transform, "GroundCheck").transform;

        HealCtrl = Instantiate(healthBar, transform);
        HealCtrl.Health = EntityStatCtrl.GetStatByID(DefineStatID.HP).FinalValue;
        Weapon = GetComponentInChildren<WeaponController>();
    }

    private void Update()
    {
        BattleEffect[] effects = new BattleEffect[1];
        effects[0] = new BattleEffect()
        {
            Type = BattleEffectType.Stun,
            Params = new float[] { 1 }
        };
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DamageMessage message = new DamageMessage()
            {
                Dame = 10,
                BattleEffects = effects
            };

            IDamageable[] damageables = this.GetComponents<IDamageable>();
            for(int i = 0; i < damageables.Length; i++)
            {
                damageables[i].TakeDame(message);
            }
        }
    }
}
