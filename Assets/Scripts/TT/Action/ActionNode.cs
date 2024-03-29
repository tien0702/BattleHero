using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class ActionNode : MonoBehaviour
    {
        protected LinkedList<ICondition> _conditions = new LinkedList<ICondition>();
        protected LinkedList<IHandle> _handles = new LinkedList<IHandle>();
        protected LinkedList<Component> _components = new LinkedList<Component>();

        public virtual int CountHandling { protected set; get; } = 0;
        public bool IsHandling => CountHandling > 0;

        // You can add a condition or a handle by this method
        public virtual void AddChild(object child)
        {
            if(child is Component)
            {
                _components.AddLast((Component)child);
            }

            if (child is ICondition)
            {
                _conditions.AddLast((ICondition)child);
            }

            else if (child is IHandle)
            {
                IHandle handle = (IHandle)child;
                _handles.AddLast(handle);
                handle.OnEndHandle = OnEndHandle;
            }
        }

        public virtual bool IsSuitable()
        {
            foreach (ICondition condition in _conditions)
            {
                if (!condition.IsSuitable) return false;
            }
            return true;
        }

        public virtual void ResetConditions()
        {
            foreach (var reset in _conditions)
            {
                reset.ResetCondition();
            }
        }

        public virtual void Action()
        {
            CountHandling = _handles.Count;
            foreach (IHandle handle in _handles)
            {
                handle.Handle();
            }
        }

        protected virtual void OnEndHandle(IHandle handle)
        {
            --CountHandling;
            if (CountHandling == 0)
            {
                ClearHandles();
            }
        }

        protected virtual void ClearHandles()
        {
            foreach (var clear in _handles)
            {
                clear.ResetHandle();
            }
        }

        protected virtual void ClearChildren()
        {
            foreach(Component component in _components)
            {
                Destroy(component);
            }
            _components.Clear();
            _conditions.Clear();
            _handles.Clear();
        }
    }
}
