using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using TT;
using System.Linq;
using Photon.Realtime;

public class RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _layoutsObj;
    Dictionary<string, GameObject> _layouts;

    [SerializeField] TextMeshProUGUI _roomCode;

    List<TeamSlotController> _teamSlots;

    public override void OnEnable()
    {
        base.OnEnable();

        // Find all layouts
        if (_layouts == null)
        {
            _layouts = new Dictionary<string, GameObject>();
            foreach (Transform child in _layoutsObj.transform)
            {
                _layouts.Add(child.gameObject.name, child.gameObject);
            }
        }

        // Disable all layouts
        foreach (Transform child in _layoutsObj.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Get roomType Property
        string roomType = PhotonNetwork.CurrentRoom.CustomProperties[HomeController.RoomTypeProperty].ToString();

        if (!_layouts.TryGetValue(roomType, out GameObject teamLayout))
        {
            Debug.Log("Can't find layout: " + roomType);
        }
        else
        {
            teamLayout.SetActive(true);
            _roomCode.text = "Room Code: " + PhotonNetwork.CurrentRoom.Name;
            _teamSlots = teamLayout.GetComponentsInChildren<TeamSlotController>().ToList();
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        foreach (var slot in _teamSlots)
        {
            if (slot.UserInfo == null)
            {
                slot.SetPlayer(null, ServiceLocator.Current.Get<User>());
                break;
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
        foreach (var slot in _teamSlots)
        {
            if (slot.UserInfo == null)
            {
                slot.SetPlayer(null, ServiceLocator.Current.Get<User>());
                break;
            }
        }
    }

    public void CopyRoomCodeToClipBoard()
    {
        GUIUtility.systemCopyBuffer = PhotonNetwork.CurrentRoom.Name;
    }
}
