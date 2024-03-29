using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SelectArenaController : MonoBehaviourPunCallbacks
{
    private bool isConnecting;

    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {

    }

    public void JoinRoomByType(string type)
    {

    }
}
