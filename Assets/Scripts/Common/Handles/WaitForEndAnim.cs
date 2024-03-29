using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;

public class WaitForEndAnim : BaseHandle, IOwn
{
    Animator _animator;

    public override void Handle()
    {
        LeanTween.delayedCall(_animator.GetCurrentAnimatorStateInfo(0).length, 
            () => { this.EndHandle(); });
    }

    public override void ResetHandle()
    {

    }

    public void SetOwn(object own)
    {
        StateController state = own as StateController;
        _animator = state.GetComponentInChildren<Animator>();
    }
}
