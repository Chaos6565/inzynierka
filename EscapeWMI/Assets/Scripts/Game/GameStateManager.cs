using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameStateManager : MonoBehaviourPun
{
    // Current Game state, can be increased to 'skip' modules when debugging.
    [SerializeField] public int _gameState = 1;
    //private int _gameState = gameState - 1;
    public int GameState { get { return _gameState; } }


    // List of all game modules, they need to be in the same order as they will be activated in the game.
    [SerializeField] private List<GameModule> gameModules = null;


    //Zmienne do sterowania ruchem postaci
    public static GameStateManager instance;
    public bool PEActive;
    public bool dialogActive;
    public bool lectureActive;



    private void Start()
    {
        instance = this;


        if (gameModules.Count >= 1)
        {
            if (_gameState < gameModules.Count)
            {
                for (int i = 0; i <= _gameState; i++)
                {
                    gameModules[i].EnableModule();
                }
                Debug.Log("Game State: " + _gameState.ToString());
            }
        }
    }

    //Do zatrzymywania graczas
    private void Update()
    {
        if (PEActive || lectureActive || dialogActive)
            PlayerController.localPlayer.canMove = false;
        else
            PlayerController.localPlayer.canMove = true;
    }

    public void ActivateNextModule()
    {
        photonView.RPC("ActivateNextModuleRPC", RpcTarget.All);
    }

    [PunRPC]
    private void ActivateNextModuleRPC()
    {
        if (_gameState + 1 < gameModules.Count)
        {
            _gameState++;
            Debug.Log("Game State: " + (_gameState).ToString());
            gameModules[_gameState].EnableModule();
        }
        else
            Debug.Log("All game modules completed.");
    }
}
