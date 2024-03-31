using UnityEngine;

namespace TT
{
    public abstract class BaseProcessController : BaseProcess
    {
        protected virtual void Display()
        {
            Vector3 currentScale = transform.localScale;
            transform.localScale = new Vector3(currentValue / maxValue, currentScale.y, currentScale.z);
        }
    }
}
