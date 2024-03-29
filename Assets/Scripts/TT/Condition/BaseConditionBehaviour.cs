using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public abstract class BaseConditionBehaviour : MonoBehaviour, ICondition
    {
        [SerializeField] protected bool isSuitable = false;
        protected Action<ICondition> onSuitable;
        public virtual bool IsSuitable => isSuitable;

        public virtual Action<ICondition> OnSuitable
        {
            set
            {
                onSuitable = value;
            }
        }

        public abstract void ResetCondition();

        protected virtual void SetSuitableCondition(bool suiable)
        {
            isSuitable = suiable;
            if (onSuitable != null)
            {
                onSuitable(this);
            }
        }
    }
}
