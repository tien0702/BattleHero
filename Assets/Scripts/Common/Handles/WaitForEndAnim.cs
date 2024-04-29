using UnityEngine;
using TT;
using System.Collections;
using Unity.VisualScripting;

public class WaitForEndAnim : BaseHandle, IOwn
{
    Animator _animator;
    float _delayTime;

    public override void Handle()
    {
        ObjectHelper.Instance.WaitOneFrame(() =>
        {
            _delayTime = _animator.GetCurrentAnimatorStateInfo(0).length;
            LeanTween.delayedCall(_delayTime,
                () => { this.EndHandle(); });
        });
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
