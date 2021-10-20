using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _roomName;
    [SerializeField]
    private Text _playerCount;

    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo) 
    {
        RoomInfo = roomInfo;
        _roomName.text = roomInfo.Name;
        _playerCount.text = roomInfo.PlayerCount.ToString() + "/" + roomInfo.MaxPlayers.ToString();
    }

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
