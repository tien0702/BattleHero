using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class HeroController : EntityController
{
    protected virtual void Awake()
    {
        this.Level = Info.Level;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {

        }
    }
}
