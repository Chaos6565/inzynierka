using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ModuleContentScript : MonoBehaviourPun
{
    private bool _isCompleted = false;
    public bool IsCompleted { get { return _isCompleted; } }


    // Is completing a task by everyone necessary for whole module completion?

    private bool moduleCompletionByEveryone = false;
    private int completionCounter = 0;

    public void ModuleCompleted()
    {
        photonView.RPC("ModuleCompletedRPC", RpcTarget.All);
    }

    [PunRPC]
    public void ModuleCompletedRPC()
    {
        if (moduleCompletionByEveryone)
        {
            completionCounter++;
            if (completionCounter >= PhotonNetwork.CurrentRoom.PlayerCount)
            {
                _isCompleted = true;
            }
        }
        else
        {
            _isCompleted = true;
        }
    }

}
