using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameModule : MonoBehaviourPun
{
    [SerializeField] ModuleContentScript _moduleContentScript;

    // Active state of CONTENT (child) of this Game Module, can be enabled/disabled throughout the duration of the game.
    // Default is false, first time enabled through GameStateManager.

    private bool _moduleActiveState = false;
    public bool ModuleActiveState { get { return _moduleActiveState; } }


    // State of module completion, when set to true, this module should not be recycled.

    private bool _moduleStateOfCompletion = false;
    public bool ModuleStateOfCompletion { get { return _moduleStateOfCompletion; } }


    private void Update()
    {
        if (!_moduleStateOfCompletion)
        {
            if (_moduleContentScript.IsCompleted)
            {
                _moduleStateOfCompletion = true;

                GetComponentInParent<GameStateManager>().ActivateNextModule();

                Debug.Log("MODULE COMPLETED!");
                DisableModule();
            } 
        }
    }

    public void EnableModule()
    {
        if (!_moduleActiveState)
            photonView.RPC("EnableModuleRPC", RpcTarget.All);  
    }

    public void DisableModule()
    {
        if (_moduleActiveState)
            photonView.RPC("DisableModuleRPC", RpcTarget.All);
    }

    [PunRPC]
    public void EnableModuleRPC()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
        }
        _moduleActiveState = true;
    }

    [PunRPC]
    public void DisableModuleRPC()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        _moduleActiveState = false;
    }
}