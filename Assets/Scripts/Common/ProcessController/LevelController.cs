using System;

namespace TT
{
    public class LevelController : BaseProcessController
    {
        public Action<int> CallbackOnLevelUp;
        protected int level;
        protected int maxLevel;

        public virtual int Level
        {
            get
            {
                return level;
            }
            set
            {
                if (value > maxLevel) return;
                currentValue = 0;
                level = value;
                if (CallbackOnLevelUp != null)
                    CallbackOnLevelUp(level);
                Display();
            }
        }

        public virtual int MaxLevel
        {
            get
            {
                return maxLevel;
            }
            set
            {
                maxLevel = value;
            }
        }

        public override float MaxValue
        {
            get => base.MaxValue;
            set
            {
                maxValue = value;
            }
        }

        public override float CurrentValue
        {
            get => base.CurrentValue;
            set
            {
                currentValue = value;
                OnChangeValue(currentValue);
            }
        }

        protected override void OnChangeValue(float value)
        {
            if (value >= maxValue)
            {
                float expLeftOver = value - maxValue;
                Level++;
                CurrentValue = expLeftOver;
            }
            Display();
        }
    }
}
