using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public abstract class BaseCondition : ICondition
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

        public virtual void ResetCondition()
        {
            SetSuitableCondition(false);
        }

        protected virtual void SetSuitableCondition(bool suitable)
        {
            isSuitable = suitable;
            if (onSuitable != null)
            {
                onSuitable(this);
            }
        }
    }
}
