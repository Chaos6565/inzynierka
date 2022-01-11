using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameModule : MonoBehaviourPun
{
    [SerializeField] ModuleContentScript _moduleContentScript;
    [SerializeField] public int[] itemIndices = null;
    ItemWorldSpawner itemWorldSpawner;

    // Active state of CONTENT (child) of this Game Module, can be enabled/disabled throughout the duration of the game.
    // Default is false, first time enabled through GameStateManager.

    private bool _moduleActiveState = false;
    public bool ModuleActiveState { get { return _moduleActiveState; } }

    // Deactivate this module after completion?
    [SerializeField] public bool deactivateModuleAfterCompletion = true;


    // State of module completion, when set to true, this module should not be recycled.

    private bool _moduleStateOfCompletion = false;
    public bool ModuleStateOfCompletion { get { return _moduleStateOfCompletion; } }

    private void Start()
    {
        itemWorldSpawner = GameObject.Find("ItemWorldSpawner").GetComponent<ItemWorldSpawner>();
    }
    private void Update()
    {
        if (!_moduleStateOfCompletion)
        {
            if (_moduleContentScript.IsCompleted)
            {
                _moduleStateOfCompletion = true;

                Debug.Log("MODULE COMPLETED!");
                if (deactivateModuleAfterCompletion)
                    DisableModule();

                GetComponentInParent<GameStateManager>().ActivateNextModule();


            } 
        }
    }

    public void EnableModule()
    {
        if (!_moduleActiveState)
            if (PhotonNetwork.IsMasterClient) 
                photonView.RPC("EnableModuleRPC", RpcTarget.All);  
    }

    public void DisableModule()
    {
        if (_moduleActiveState)
            if (PhotonNetwork.IsMasterClient)
                photonView.RPC("DisableModuleRPC", RpcTarget.All);
    }

    [PunRPC]
    public void EnableModuleRPC()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
        }

        if (this.itemIndices.Length > 0)
        {

            foreach (int index in this.itemIndices)
            {
                itemWorldSpawner.SpawnItemWorld(index);
            }
        }

        _moduleActiveState = true;
    }

    [PunRPC]
    public void DisableModuleRPC()
    {
        if (PhotonNetwork.IsMasterClient)
            itemWorldSpawner.DestroyAllItemsWorld();
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        _moduleActiveState = false;
    }
}
