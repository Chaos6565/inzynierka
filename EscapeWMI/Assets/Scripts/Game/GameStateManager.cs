using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameStateManager : MonoBehaviourPun
{
    // Current Game state, can be increased to 'skip' modules when debugging.
    [SerializeField] public int _gameState = 0;
    public int GameState { get { return _gameState; } }


    // List of all game modules, they need to be in the same order as they will be activated in the game.
    [SerializeField] private List<GameModule> gameModules = null;

    [SerializeField] public List<int> idOfGameStatesToSkip = null;

    //Zmienne do sterowania ruchem postaci
    public static GameStateManager instance;
    public bool PEActive;
    public bool dialogActive;
    public bool lectureActive;
    public bool choiceDialog;
    public bool programmingActive;
    public bool taskActive;
    public bool menuActive;

    private void Start()
    {
        instance = this;


        /*if (gameModules.Count >= 1)
        {
            if (_gameState < gameModules.Count)
            {
                for (int i = 0; i <= _gameState; i++)
                {
                    gameModules[i].EnableModule();
                }
                Debug.Log("Game State: " + _gameState.ToString());
            }
        }*/
        Debug.Log("Game State at Start: " + (_gameState).ToString());
        gameModules[_gameState].EnableModule();
    }

    //Do zatrzymywania gracza
    private void Update()
    {
        try
        {
            if (PhotonNetwork.LocalPlayer.IsLocal)
            {
                if (PEActive || lectureActive || dialogActive || choiceDialog || programmingActive || taskActive || menuActive)
                    PlayerController.localPlayer.canMove = false;
                else
                    PlayerController.localPlayer.canMove = true;
            }
        }
        catch
        {
        }
    }

    public void AddToSkipList(int gameStateId)
    {
        photonView.RPC("RPCAddToSkipList", RpcTarget.All, gameStateId);
    }

    [PunRPC]
    public void RPCAddToSkipList(int gameStateId)
    {
        idOfGameStatesToSkip.Add(gameStateId);
    }

    public void ActivateNextModule()
    {
        if (PhotonNetwork.IsMasterClient)
            photonView.RPC("ActivateNextModuleRPC", RpcTarget.All);
    }

    [PunRPC]
    private void ActivateNextModuleRPC()
    {
        if (_gameState + 1 < gameModules.Count)
        {
            _gameState += 1;

            if (idOfGameStatesToSkip != null)
            {
                while (true)
                {
                    if (idOfGameStatesToSkip.Contains(_gameState))
                    {
                        if (_gameState + 1 < gameModules.Count)
                        {
                            _gameState += 1;
                        }
                        else
                        {
                            Debug.Log("All game modules completed.");
                            break;
                        }
                    } else
                    {
                        break;
                    }
                }
            }
            Debug.Log("Game State: " + (_gameState).ToString());
            gameModules[_gameState].EnableModule();
        }
        else
            Debug.Log("All game modules completed.");
    }
}
