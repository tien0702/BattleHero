using System;

namespace TT
{
    public class HealthController : BaseProcessController
    {
        public Action OnDie;

        public virtual float Health
        {
            get => currentValue;
            set
            {
                MaxValue = value;
                CurrentValue = maxValue;
                Display();
            }
        }

        public override float CurrentValue
        {
            get => base.CurrentValue;
            set
            {
                base.CurrentValue = value;
                OnChangeValue(currentValue);
            }
        }

        protected override void OnChangeValue(float value)
        {
            if (value == 0 && OnDie != null) OnDie();
            Display();
        }
    }
}
