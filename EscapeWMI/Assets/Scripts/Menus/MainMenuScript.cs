using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class MainMenuScript : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameRoomList _gameRoomList;
    public GameRoomList GameRoomList { get { return _gameRoomList; } }
    [SerializeField]
    private InRoomPlayerList _inRoomPlayerList;
    public InRoomPlayerList InRoomPlayerList { get { return _inRoomPlayerList; } }


    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        FirstInitialize();
    }

    public void OnClick_ConnectToServer()
    {
        Debug.Log("Connecting to server ...", this);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.ConnectionSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.ConnectionSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected as: " + PhotonNetwork.LocalPlayer.NickName);
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason: " + cause.ToString());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
    }

    private void FirstInitialize()
    {
        GameRoomList.FirstInitialize(this);
        InRoomPlayerList.FirstInitialize(this);
    }

    public void ExitTheGame()
    {
        Debug.Log("Exiting the game.");
        Application.Quit();
    }

}
