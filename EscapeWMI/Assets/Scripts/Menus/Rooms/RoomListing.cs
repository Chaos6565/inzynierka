using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _roomName;
    [SerializeField]
    private TMP_Text _playerCount;

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
