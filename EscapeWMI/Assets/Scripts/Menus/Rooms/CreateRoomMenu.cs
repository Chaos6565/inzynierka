using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private InputField _roomName;

    [SerializeField] private const int MaxPlayersPerRoom = 3;

    private MainMenuScript _mainMenuScript;

    public void FirstInitialize(MainMenuScript mainMenuScriptReference)
    {
        _mainMenuScript = mainMenuScriptReference;
    }

    public void OnClick_CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomName.text)) { return; }

        if (!PhotonNetwork.IsConnected) { return; }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = MaxPlayersPerRoom;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully.", this);
        _mainMenuScript.InRoomPlayerList.Show();
        _mainMenuScript.GameRoomList.Hide();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message, this);
    }

}
