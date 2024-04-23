using System.Collections;
using TT;
using UnityEngine;

[System.Serializable]
public class MoveOnAttackInfo
{
    public float Range;
    public float Force;
    public float LookAngle;
}

public class MoveOnAttack : BaseHandle, IInfo, IOwn
{
    public MoveOnAttackInfo Info { private set; get; }

    //References
    FilterTargetController _filterCtrl;
    Rigidbody _rb;
    Transform _model;
    Animator _animator;

    //Informations
    public override void Handle()
    {
        //setup
        _rb.velocity = Vector3.zero;

        var target = TargetController.FindBestTarget(_filterCtrl);
        Vector3 direction = target.position - _rb.position;

        // Move by joystick
        /*if (direction.magnitude > Info.Range || Vector3.Angle(direction, _model.forward) > Info.LookAngle)
        {
            float force = direction.magnitude;
            if (direction.magnitude > Info.Range || Vector3.Angle(direction, _model.forward) > Info.LookAngle)
            {
                direction.x = _joyMove.Direction.x;
                direction.z = _joyMove.Direction.y;
                force = Info.Force;
            }
            // Move by model
            if (direction == Vector3.zero)
                direction = _model.forward;
            direction.y = 0;


            if (!direction.Equals(Vector3.zero)) _model.forward = direction;
            _rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        }
        else
        {
            // Move to target
            direction = Vector3.zero * direction.magnitude * 0.8f;

            var clip = _animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            var clips = _animator.GetCurrentAnimatorClipInfo(0);

            LeanTween.move(_rb.gameObject, Vector3.zero, clip.events[0].time);
        }*/

        float force = direction.magnitude;
        if (direction.magnitude > Info.Range || Vector3.Angle(direction, _model.forward) > Info.LookAngle)
        {
            direction = _model.forward;
            force = Info.Force;
        }

        direction.y = 0;

        if (!direction.Equals(Vector3.zero)) _model.forward = direction;
        _rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        EndHandle();
    }

    public override void ResetHandle()
    {

    }

    public void SetInfo(object info)
    {
        if (info is MoveOnAttackInfo)
        {
            this.Info = (MoveOnAttackInfo)info;
        }
    }

    public void SetOwn(object own)
    {
        StateController state = own as StateController;
        _rb = state.GetComponent<Rigidbody>();
        _filterCtrl = state.GetComponent<FilterTargetController>();
        _model = GameObjectUtilities.FindObjectByTag(state.transform, "Model")?.transform;
        _animator = _model.GetComponent<Animator>();
    }
}
