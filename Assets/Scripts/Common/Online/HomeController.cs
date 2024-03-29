using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using TT;
using UnityEngine;

public class HomeController : MonoBehaviourPunCallbacks
{
    public const string RoomTypeProperty = "RoomType";
    public string[] RoomTypes = new string[] { "DUAL", "BATTLE_ROYAL" };

    public GameObject _roomList, roomPreb;

    [SerializeField] private GameObject _searchingPanel;
    [SerializeField] private GameObject _roomCtrlPanel;

    List<RoomInfo> _rooms = new List<RoomInfo>();
    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();

            User user = new User() { NickName = GenerateRandomString(5) };
            ServiceLocator.Current.Register(user);
        }
    }

    private void Start()
    {
        _searchingPanel.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        var user = ServiceLocator.Current.Get<User>();
        Debug.Log("connect to master!");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.LocalPlayer.NickName = user.NickName;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void JoinRandomRoomByType(string roomType)
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogWarning("PhotoNetwork is not connected!");
            return;
        }

        List<RoomInfo> rooms = _rooms.FindAll(r =>
            (r.CustomProperties != null) &&
            (r.CustomProperties.TryGetValue(RoomTypeProperty, out object val) &&
            (val as string).Equals(roomType))
        );
        if (rooms.Count <= 0 && !RoomTypes.Any(r => r.Equals(roomType)))
        {
            Debug.Log("RoomType is not valid!");
            return;
        }

        foreach (RoomInfo room in rooms)
        {
            if (room.PlayerCount >= room.MaxPlayers) continue;
            PhotonNetwork.JoinRoom(room.Name);
            return;
        }

        CreateAndJoinRoomByType(roomType);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        _searchingPanel.SetActive(false);
        _roomCtrlPanel.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {

    }

    public void CreateAndJoinRoomByType(string roomType)
    {
        if (!RoomTypes.Any(r => r.Equals(roomType)))
        {
            Debug.Log("RoomType is not valid!");
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();

        if (RoomTypes[0].Equals(roomType))
        {
            roomOptions.MaxPlayers = 2;
        }
        else
        {
            roomOptions.MaxPlayers = 8;
        }

        string randomRoomCode = GenerateRandomString(8);

        Debug.Log("CreateRoom: " + randomRoomCode);
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable();
        customRoomProperties.Add(HomeController.RoomTypeProperty, roomType);
        roomOptions.CustomRoomProperties = customRoomProperties;
        PhotonNetwork.CreateRoom(randomRoomCode, roomOptions);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo room in  roomList)
        {
            if (room.RemovedFromList) _rooms.Remove(room);
            else _rooms.Add(room);
        }

        foreach(Transform child in _roomList.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(RoomInfo roomInfo in _rooms)
        {
            Instantiate(roomPreb, _roomList.transform).GetComponent<TextMeshProUGUI>().text = roomInfo.Name;
        }
        Debug.Log("room list update - count: " + _rooms.Count);
    }

    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder stringBuilder = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(chars[Random.Range(0, chars.Length)]);
        }

        return stringBuilder.ToString();
    }
}
