using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using TT;
using System.Linq;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _layoutsObj, _selectArenaBtn, _startBtn;
    Dictionary<string, GameObject> _layouts;

    [SerializeField] TextMeshProUGUI _roomCode;

    List<TeamSlotController> _teamSlots;

    public override void OnEnable()
    {
        base.OnEnable();

        // Check connect photon
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogWarning("Photon is not connected!");
            return;
        }

        // Display MasterClient's buttons
        if (PhotonNetwork.IsMasterClient)
        {
            _selectArenaBtn.SetActive(true);
            _startBtn.SetActive(true);
        }
        else
        {
            _selectArenaBtn.SetActive(false);
            _startBtn.SetActive(false);
        }

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
        string roomType = PhotonNetwork.CurrentRoom.CustomProperties[HomeSceneService.RoomTypeProperty].ToString();

        if (!_layouts.TryGetValue(roomType, out GameObject teamLayout))
        {
            Debug.Log("Can't find layout: " + roomType);
        }
        else
        {
            teamLayout.SetActive(true);
            _roomCode.text = PhotonNetwork.CurrentRoom.Name;
            _teamSlots = teamLayout.GetComponentsInChildren<TeamSlotController>().ToList();
        }

        Hashtable playerCustomProps = new Hashtable();
        playerCustomProps.Add("avtName", "");
        playerCustomProps.Add("heroName", "");
        playerCustomProps.Add("heroLv", 1);
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerCustomProps);
        UpdateSlotInfo();
    }

    public void ToggleTeamParticipation(bool value)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsVisible = value;
        }
    }

    public override void OnLeftRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.MasterClient.GetNext());
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateSlotInfo();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateSlotInfo();
    }

    void UpdateSlotInfo()
    {
        Debug.Log("UpdateSlotInfo");
        Player[] players = PhotonNetwork.PlayerList;
        _teamSlots.ForEach(t => t.UpdateTeamSlotInfomation(null, null, null, 1));
        for (int i = 0; i < players.Length; ++i)
        {
            _teamSlots[i].UpdateTeamSlotInfomation(players[i].NickName, "", "", (byte)1);
        }
    }

    public void CopyRoomCodeToClipBoard()
    {
        GUIUtility.systemCopyBuffer = PhotonNetwork.CurrentRoom.Name;
    }
}
