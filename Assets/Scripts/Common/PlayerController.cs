using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : HeroController
{
    protected override void Awake()
    {
        Info = GlobalData.CharacterSelected;
        base.Awake();
    }
}
