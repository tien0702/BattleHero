using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

public class PlayerController : HeroController
{
    HealthController healthController;
    public void SetInfo(EntityInfo info)
    {
        this.Info = info;
        this.Level = Info.Level;
    }

    private void Start()
    {
        var healthBar = Resources.Load<HealthController>("Prefabs/UI/HeroHealthBar");

        healthController = Instantiate(healthBar, transform);
        healthController.transform.localPosition = new Vector3(0, 2, 0);
    }
}
