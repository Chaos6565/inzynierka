using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviourPun
{
    [SerializeField]
    private Text _playerNickName;
    [SerializeField]
    private Text _readyStatusText;

    public Player Player { get; private set; }
    public bool readyStatus = false;

    public void SetPlayerInfo(Player player) 
    {
        Player = player;
        _playerNickName.text = Player.NickName;
        if (Player.IsMasterClient)
            _readyStatusText.text = "Host";
        else
            _readyStatusText.text = "";
    }

    public void SetReadyStatus(bool ready)
    {
        readyStatus = ready;
        if (readyStatus)
            _readyStatusText.text = "Gotowy";
        else
            _readyStatusText.text = "";
    }
}
