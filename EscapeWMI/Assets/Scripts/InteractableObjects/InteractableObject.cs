using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class InteractableObject : MonoBehaviourPun
{
    protected bool enabledState = true;

    public virtual void PerformAction() { }

    public bool IsEnabled()
    {
        return enabledState;
    }

    public void EnableIntaraction()
    {
        enabledState = true;
    }

    public void DisableInteraction()
    {
        enabledState = false;
    }

    public void EnableInteractionForAll()
    {
        photonView.RPC("EnableInteractionForAllRPC", RpcTarget.All);
    }

    public void DisableInteractionForAll()
    {
        photonView.RPC("DisableInteractionForAllRPC", RpcTarget.All);
    }

    [PunRPC]
    public void EnableInteractionForAllRPC()
    {
        enabledState = true;
    }

    [PunRPC]
    public void DisableInteractionForAllRPC()
    {
        enabledState = false;
    }

}

