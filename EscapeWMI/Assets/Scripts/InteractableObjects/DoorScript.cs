using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WMI
{
    public class DoorScript : MonoBehaviourPun
    {
        // Components
        private Animator doorAnimation = null;
        private BoxCollider2D doorCollider = null;
        private SpriteRenderer doorSpriteRenderer = null;

        // Parameter
        bool isOpen = false;

        private void Start()
        {
            doorAnimation = this.GetComponent<Animator>();
            doorCollider = this.GetComponent<BoxCollider2D>();
            doorSpriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        public void OpenTheDoor()
        {
            if (!isOpen)
            {
                photonView.RPC("OpenTheDoorRPC", RpcTarget.All);
            }
        }

        public void CloseTheDoor()
        {
            if (isOpen)
            {
                photonView.RPC("CloseTheDoorRPC", RpcTarget.All);
            }
        }

        [PunRPC]
        void OpenTheDoorRPC()
        {
            if (!isOpen)
            {
                doorAnimation.SetBool("isOpen", true);
                doorCollider.enabled = false;
                isOpen = true;
            }
        }

        [PunRPC]
        void CloseTheDoorRPC()
        {
            if (isOpen)
            {
                doorAnimation.SetBool("isOpen", false);
                doorCollider.enabled = true;
                isOpen = false;
            }
        }

        public bool IsTheDoorOpen()
        {
            return isOpen;
        }

    }
}