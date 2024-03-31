using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TT
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    public class LevelCanvasController : LevelController
    {
        protected Image fill;
        [SerializeField] protected TextMeshProUGUI processInfoText;
        [SerializeField] protected TextMeshProUGUI levelText;

        protected virtual void Awake()
        {
            fill = GetComponent<Image>();
        }

        protected override void Display()
        {
            fill.fillAmount = currentValue / maxValue;
            if(levelText != null)
            {
                levelText.text = "Lv. " + level.ToString();
            }

            if(processInfoText != null)
            {
                processInfoText.text = currentValue.ToString() + " / " + maxValue.ToString();
            }
        }
    }
}