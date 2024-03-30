using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using TT;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class HomeController : MonoBehaviourPunCallbacks
{
    public const string RoomTypeProperty = "RoomType";
    public string[] RoomTypes = new string[] { "DUAL", "BATTLE_ROYAL" };

    private string _roomTypeToJoin;

    public TypedLobby Lobby { get; private set; } = new TypedLobby("Lobby", LobbyType.Default);

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
            PhotonNetwork.LocalPlayer.NickName = user.NickName;
        }

        _roomTypeToJoin = RoomTypes[0];
    }

    private void Start()
    {
        _searchingPanel.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connect to master!");
        PhotonNetwork.JoinLobby();
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
        _roomTypeToJoin = roomType;
        Hashtable expectedCustomProperties = new Hashtable();
        expectedCustomProperties.Add(RoomTypeProperty, _roomTypeToJoin);

        PhotonNetwork.JoinRandomRoom(expectedCustomProperties, _roomTypeToJoin.Equals(RoomTypes[0]) ? (byte)(2) : (byte)(8));
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoomByType(_roomTypeToJoin);
    }

    public void CreateRoomByType(string roomType)
    {
        RoomOptions roomOptions = new RoomOptions();

        Hashtable customRoomProperties = new Hashtable();
        customRoomProperties.Add(RoomTypeProperty, roomType);
        roomOptions.CustomRoomProperties = customRoomProperties;
        roomOptions.CustomRoomPropertiesForLobby = new string[] { RoomTypeProperty };
        roomOptions.MaxPlayers = roomType.Equals(RoomTypes[0]) ? (byte)(2) : (byte)(8);
        string randomRoomCode = GenerateRandomString(8);
        Debug.Log("CreateRoom: " + randomRoomCode);
        PhotonNetwork.CreateRoom(randomRoomCode, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        _searchingPanel.SetActive(false);
        _roomCtrlPanel.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("room list update - count: " + _rooms.Count);
        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList) _rooms.Remove(room);
            else if (!_rooms.Contains(room)) _rooms.Add(room);
        }

        foreach (Transform child in _roomList.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (RoomInfo roomInfo in _rooms)
        {
            Instantiate(roomPreb, _roomList.transform).GetComponent<TextMeshProUGUI>().text = roomInfo.Name;
        }
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
