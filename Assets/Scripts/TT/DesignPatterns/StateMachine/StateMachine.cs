using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SimpleJSON;

namespace TT
{
    public class StateMachine : BaseEntity, IInfo
    {
        #region Events
        public enum StateMachineEventType { OnChangeState };
        public ObserverEvents<StateMachineEventType, StateController> Events { protected set; get; }
        = new ObserverEvents<StateMachineEventType, StateController>();
        #endregion

        Dictionary<int, StateController> _states = new Dictionary<int, StateController>();

        [field: SerializeField] public int CurrentState { protected set; get; }
        public StateController[] States => _states.Values.ToArray();

        protected virtual void Start()
        {
            _states[CurrentState].OnEnter();
        }

        protected virtual void Update()
        {
            int state = _states[CurrentState].OnUpdate(Time.deltaTime);
            if (state.Equals(CurrentState)) return;
            _states[CurrentState].OnExit();
            CurrentState = state;
            Events.Notify(StateMachineEventType.OnChangeState, _states[CurrentState]);
            _states[CurrentState].OnEnter();
        }

        public void SetInfo(object info)
        {
            if (info is EntityInfo)
            {
                this.Info = (EntityInfo)info;
                Level = Info.Level;
            }
        }

        protected override void OnAddChild(object child)
        {
            base.OnAddChild(child);
            if (child is StateController)
            {
                var state = child as StateController;
                if (_states.Count == 0)
                {
                    CurrentState = state.Info.StateHash.State;
                }
                _states.Add(state.Info.StateHash.State, state);
            }
        }

        public StateController GetState(int stateHash)
        {
            if (!_states.ContainsKey(stateHash))
            {
                return null;
            }
            return _states[stateHash];
        }

        public StateController GetStateByName(string stateName)
        {
            foreach (var state in _states)
            {
                if (state.Value.Info.StateName.Equals(stateName))
                {
                    return state.Value;
                }
            }
            return null;
        }

        protected override void OnLevelUp(int level)
        {
            JSONNode data = ServiceLocator.Current.Get<DataService>().GetByEntityInfo(Info);
            if (data == null)
            {
                Debug.Log($"Data at level {level} is null!");
            }
            else if (!data.IsArray)
            {
                Debug.Log($"Data at level {level} is not array!");
            }
            else
            {
                this.ClearChildren();
                JSONArray array = data.AsArray;
                var nodeTypes = NodeUtils.GetNodeTypes(array);
                ComponentUtils.HandleNodes(gameObject, nodeTypes, OnAddChild);
            }
        }
    }
}
