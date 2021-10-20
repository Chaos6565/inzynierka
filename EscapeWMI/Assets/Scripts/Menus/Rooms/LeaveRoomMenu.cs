using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LeaveRoomMenu : MonoBehaviour
{

    private MainMenuScript _mainMenuScript;

    public void FirstInitialize(MainMenuScript mainMenuScriptReference)
    {
        _mainMenuScript = mainMenuScriptReference;
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        _mainMenuScript.InRoomPlayerList.Hide();
        _mainMenuScript.GameRoomList.Show();
        Debug.Log("Left the room");
    }

    
}
