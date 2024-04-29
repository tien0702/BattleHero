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
    Rigidbody _rb;
    Transform _model;
    JoystickController _moveJoy;

    //Informations
    float _keepDistance = 0.5f;
    float _maxMoveRange = 2f;
    float _timeMove = 0.1f;
    ICheckTarget[] _checkTargets;
    ICheckBestTarget[] _checkBestTargets;
    public override void Handle()
    {
        //setup
        _rb.velocity = Vector3.zero;

        var target = TargetController.FindTarget(_checkTargets, _checkBestTargets);

        if (target != null)
        {
            Vector3 direction = target.position - _rb.position;
            float distance = direction.magnitude;

            if (Vector3.Angle(direction, _model.forward) > Info.LookAngle)
            {
                if (_moveJoy.IsControl)
                    _model.forward = new Vector3(_moveJoy.Direction.x, 0, _moveJoy.Direction.y);
                direction = _model.forward;
                _rb.AddForce(direction * Info.Force, ForceMode.Impulse);
            }
            else
            {
                _model.forward = direction;
                if (distance >= _maxMoveRange) distance = _maxMoveRange - _keepDistance;
                else distance = Mathf.Max(0, distance - _keepDistance);

                direction = _model.forward * distance;
                direction.y = _rb.transform.position.y;
                _rb.transform.LeanMove(_rb.transform.position + direction, _timeMove);
            }
        }
        else
        {
            if (_moveJoy.IsControl)
                _model.forward = new Vector3(_moveJoy.Direction.x, 0, _moveJoy.Direction.y);
            Vector3 direction = _model.forward;
            _rb.AddForce(direction * Info.Force, ForceMode.Impulse);
        }

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
        _model = GameObjectUtilities.FindObjectByTag(state.transform, "Model")?.transform;
        _moveJoy = JoystickController.GetJoystick("Move");

        _checkTargets = new ICheckTarget[1];
        _checkBestTargets = new ICheckBestTarget[1];
        _checkTargets[0] = new InRangeTarget() { Own = _rb.transform, Range = Info.Range };
        _checkBestTargets[0] = new NearestTarget() { Own = _rb.transform };
    }
}
