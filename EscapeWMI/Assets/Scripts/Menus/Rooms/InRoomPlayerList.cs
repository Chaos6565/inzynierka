using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class InRoomPlayerList : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PlayerListingsMenu _playerListingsMenu;
    [SerializeField]
    public GameObject _startButton;
    [SerializeField]
    public GameObject _readyButton;


    private MainMenuScript _mainMenuScript;

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        SetUpNavigationButtons(PhotonNetwork.IsMasterClient);
    }

    public override void OnJoinedRoom()
    {
        SetUpNavigationButtons(PhotonNetwork.IsMasterClient);
    }

    private void SetUpNavigationButtons(bool mode)
    {
        _startButton.gameObject.SetActive(mode);
        _readyButton.gameObject.SetActive(!mode);
    }

    public void FirstInitialize(MainMenuScript mainMenuScriptReference)
    {
        _mainMenuScript = mainMenuScriptReference;
        _playerListingsMenu.FirstInitialize(mainMenuScriptReference);
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        SetUpNavigationButtons(false);
        Hide();
        _mainMenuScript.GameRoomList.Show();
        Debug.Log("Left the room");
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
