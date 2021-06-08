using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


namespace WMI
{
    public class InteractableObject : MonoBehaviourPun
    {
        protected bool enabledState = true;

        public virtual void PerformAction() { }

        public void EnableInteraction()
        {
            photonView.RPC("EnableInteractionRPC", RpcTarget.All);
        }

        public void DisableInteraction()
        {
            photonView.RPC("DisableInteractionRPC", RpcTarget.All);
        }

        public bool IsEnabled()
        {
            return enabledState;
        }

        [PunRPC]
        public void EnableInteractionRPC()
        {
            enabledState = true;
        }

        [PunRPC]
        public void DisableInteractionRPC()
        {
            enabledState = false;
        }

    }
}

