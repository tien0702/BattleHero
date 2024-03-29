using UnityEngine;

namespace TT
{
    [System.Serializable]
    public class PlayAnimInfo
    {
        public string AnimName;
    }

    public class PlayAnim : IOwn, IInfo
    {
        public PlayAnimInfo Info { private set; get; }
        Animator _animator;
        StateController _state;

        int _animHash;

        void OnEnter(StateController state)
        {
            _animator.Play(_animHash);
        }

        public void SetOwn(object own)
        {
            if(own is StateController)
            {
                this._state = (StateController)own;
                _animator = _state.GetComponentInChildren<Animator>();
                _state.Events.RegisterEvent(StateController.StateEventType.OnEnter, OnEnter);
            }
        }

        public void SetInfo(object info)
        {
            if(info is PlayAnimInfo)
            {
                Info = (PlayAnimInfo)info;
                _animHash = Animator.StringToHash(Info.AnimName);
            }
        }
    }
}
