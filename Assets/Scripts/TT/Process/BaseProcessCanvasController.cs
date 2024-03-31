using UnityEngine;
using UnityEngine.UI;

namespace TT
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    public abstract class BaseProcessCanvasController : BaseProcess
    {
        [SerializeField] protected Image fill;
        protected virtual void Awake()
        {
            fill = GetComponent<Image>();
        }

        protected virtual void Display()
        {
            fill.fillAmount = currentValue / maxValue;
        }
    }
}
