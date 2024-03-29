using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;

namespace TT
{
    public abstract class BaseEntity : MonoBehaviour
    {
        [field: SerializeField] public EntityInfo Info { protected set; get; }

        LinkedList<object> _children = new LinkedList<object>();

        public int Level
        {
            protected set
            {
                Info.Level = value;
                OnLevelUp(Level);
            }
            get
            {
                return Info.Level;
            }
        }

        protected abstract void OnLevelUp(int level);

        protected virtual void OnAddChild(object child)
        {
            if(child != null) _children.AddLast(child);
        }

        protected virtual void ClearChildren()
        {
            foreach(object child in _children)
            {
                if(child is Component) Destroy(child as Component);
            }
        }
    }
}
