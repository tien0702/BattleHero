using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TT;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Events
    public enum ButtonEvent { OnDown, OnUp, OnCancel }
    public ObserverEvents<ButtonEvent, ButtonEvent> Events { private set; get; }
        = new ObserverEvents<ButtonEvent, ButtonEvent>();
    #endregion

    public string ButtonID;

    public void OnPointerDown(PointerEventData eventData)
    {
        Events.Notify(ButtonEvent.OnDown, ButtonEvent.OnDown);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Events.Notify(ButtonEvent.OnUp, ButtonEvent.OnUp);
    }
}
