using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public interface IOwn
    {
        void SetOwn(object own);
    }

    [System.Serializable]
    public class StateInfo
    {
        public string StateName;
        public string[] InterruptibleStates;
        public string[] NextStates;
        private StateInfoHash _stateHash;
        public StateInfoHash StateHash
        {
            get
            {
                if (_stateHash == null)
                {
                    _stateHash = new StateInfoHash(this);
                }
                return _stateHash;
            }
        }
    }

    public class StateInfoHash
    {
        public int State;
        public int[] InterruptibleStates;
        public int[] NextStates;

        public StateInfoHash(StateInfo stateInfo)
        {
            State = Animator.StringToHash(stateInfo.StateName);
            InterruptibleStates = new int[stateInfo.InterruptibleStates.Length];
            for (int i = 0; i < InterruptibleStates.Length; ++i)
            {
                InterruptibleStates[i] = Animator.StringToHash(stateInfo.InterruptibleStates[i]);
            }

            NextStates = new int[stateInfo.NextStates.Length];
            for (int i = 0; i < NextStates.Length; ++i)
            {
                NextStates[i] = Animator.StringToHash(stateInfo.NextStates[i]);
            }
        }
    }

    public class StateController : ActionNode, IInfo
    {
        #region Events
        public enum StateEventType { OnEnter, OnUpdate, OnExit, OnCheck, OnClear }
        public ObserverEvents<StateEventType, StateController> Events { protected set; get; }
         = new ObserverEvents<StateEventType, StateController>();
        #endregion

        [field: SerializeField] public virtual StateInfo Info { private set; get; }

        protected StateMachine _stateMachine;

        protected LinkedList<IOwn> _owns = new LinkedList<IOwn>();

        protected virtual void Start()
        {
            foreach(IOwn own in _owns) own.SetOwn(this);
        }

        public override void AddChild(object child)
        {
            base.AddChild(child);
            if (child is IOwn)
            {
                _owns.AddLast((IOwn)child);
            }
        }

        public void SetInfo(object data)
        {
            if (data is StateInfo)
            {
                this.Info = (StateInfo)data;
                DataLayer _dl = ServiceLocator.Current.Get<DataService>().GetDL("State");

                _stateMachine = GetComponent<StateMachine>();
                JSONArray array = _dl.GetWithIndex($"{_stateMachine.Info.Name}-{this.Info.StateName}", 0).AsArray;
                var nodeTypes = NodeUtils.GetNodeTypes(array);
                ComponentUtils.HandleNodes(gameObject, nodeTypes, AddChild);
            }
        }

        public virtual void OnEnter()
        {
            Events.Notify(StateEventType.OnEnter, this);
            ResetConditions();  
            Action();
        }

        public virtual int OnUpdate(float deltaTime)
        {
            Events.Notify(StateEventType.OnUpdate, this);

            // Check Interruptible States
            for (int i = 0; i < Info.InterruptibleStates.Length; i++)
            {
                StateController nextState = _stateMachine.GetState(Info.StateHash.InterruptibleStates[i]);
                if (nextState != null && nextState.IsSuitable())
                {
                    return Info.StateHash.InterruptibleStates[i];
                }
            }

            // Check Handling
            if (this.IsHandling) return Info.StateHash.State;

            // Check Next States
            for (int i = 0; i < Info.NextStates.Length; i++)
            {
                StateController nextState = _stateMachine.GetState(Info.StateHash.NextStates[i]);
                if (nextState != null && nextState.IsSuitable())
                {
                    return Info.StateHash.NextStates[i];
                }
            }

            return Info.StateHash.State;
        }

        public virtual void OnExit()
        {
            Events.Notify(StateEventType.OnExit, this);
            this.ClearHandles();
        }

        protected override void ClearChildren()
        {
            base.ClearChildren();

            Events.Notify(StateEventType.OnClear, this);
            Events.Clear();
        }

        public override bool IsSuitable()
        {
            Events.Notify(StateEventType.OnCheck, this);
            return base.IsSuitable();
        }
    }
}
