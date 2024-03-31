using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TT
{
    public class HealthCanvasController : HealthController
    {
        [SerializeField] protected Image _healthProcess;
        [SerializeField] protected TextMeshProUGUI _healthText;

        private void Awake()
        {
            if(_healthProcess == null) _healthProcess = GetComponent<Image>();
            if(_healthText == null) _healthText = GetComponentInChildren<TextMeshProUGUI>();
        }

        protected override void Display()
        {
            _healthProcess.fillAmount = currentValue / maxValue;
            if (_healthText != null)
            {
                _healthText.text = currentValue.ToString() + " / " + maxValue.ToString();
            }
        }
    }
}
