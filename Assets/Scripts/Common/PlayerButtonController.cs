using System.Collections;
using System.Collections.Generic;
using TT;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonController : MonoBehaviour, IGameService
{
    [SerializeField] private Dictionary<string, ButtonController> _buttons = new Dictionary<string, ButtonController>();

    private void Awake()
    {
        ButtonController[] buttons = GetComponentsInChildren<ButtonController>();
        foreach (ButtonController button in buttons)
        {
            _buttons.Add(button.ButtonID, button);
        }
    }

    public ButtonController GetByID(string buttonID)
    {
        _buttons.TryGetValue(buttonID, out ButtonController button);
        return button;
    }

}
