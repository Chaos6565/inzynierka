using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using TMPro;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _content;
    [SerializeField] private PlayerListing _playerListing;
    [SerializeField] private int _minPlayersPerRoom = 0;
    private MainMenuScript _mainMenuScript;

    private List<PlayerListing> _listings = new List<PlayerListing>();

    private bool _ready = false;

    private void Awake()
    {
        _minPlayersPerRoom = MasterManager.ConnectionSettings.MinPlayersPerRoom;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        GetCurrentRoomPlayers();
        SetReadyUp(false);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < _listings.Count; i++)
            Destroy(_listings[i].gameObject);
        _listings.Clear();
    }

    public void FirstInitialize(MainMenuScript mainMenuScriptReference)
    {
        _mainMenuScript = mainMenuScriptReference;
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
        
    }

    private void SetReadyUp(bool state)
    {
        _ready = state;
    }

    private void AddPlayerListing(Player newPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == newPlayer);
        if (index != -1)
        {
            _listings[index].SetPlayerInfo(newPlayer);
        }
        else
        {
            PlayerListing listing = Instantiate(_playerListing, _content);
            if (listing != null)
            {
                listing.SetPlayerInfo(newPlayer);
                _listings.Add(listing);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
        Debug.Log(newPlayer.NickName + " entered the room.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Debug.Log(_listings[index].Player.NickName + " left the room");
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        int index = _listings.FindIndex(x => x.Player == newMasterClient);
        if (index != -1)
        {
            _listings[index].SetPlayerInfo(newMasterClient);
        }
    }

        public void OnClick_StartGame()
    {
        if (PhotonNetwork.CountOfPlayers < _minPlayersPerRoom) { Debug.Log("Not enough players in room"); return; }
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < _listings.Count; i++)
            {
                if (_listings[i].Player != PhotonNetwork.LocalPlayer)
                {
                    if (!_listings[i].readyStatus)
                        return;
                }
            }

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }

    public void OnClick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            photonView.RPC("RPC_ChangeReadyState", RpcTarget.All, PhotonNetwork.LocalPlayer, _ready);
        }
    }

    [PunRPC]
    public void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].SetReadyStatus(ready);
        }
    }
}
