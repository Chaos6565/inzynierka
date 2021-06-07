using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WMI
{
    public class DoorScript : InteractableObject
    {
        [SerializeField] private Animator lowerAnimation = null;
        [SerializeField] private Animator leftAnimation = null;

        [SerializeField] private GameObject lowerDoor = null;
        [SerializeField] private GameObject leftDoor = null;

        [SerializeField] private GameObject trigger = null;

        [PunRPC]
        void OpenLowerDoor()
        {
            lowerAnimation.SetBool("isOpen", true);
            lowerDoor.GetComponent<BoxCollider2D>().enabled = false;
            trigger.SetActive(false);
            //this.DisableInteraction();
        }
        [PunRPC]
        void OpenLeftDoor()
        {
            leftAnimation.SetBool("isOpen", true);
            leftDoor.GetComponent<BoxCollider2D>().enabled = false;
            trigger.SetActive(false);
            //this.DisableInteraction();
        }

        public override void PerformAction()
        {
            if (leftAnimation.GetBool("isOpen") == false)
            {
                photonView.RPC("OpenLeftDoor", RpcTarget.All);
            }
            if (lowerAnimation.GetBool("isOpen") == false)
            {
                photonView.RPC("OpenLowerDoor", RpcTarget.All);
            }
        }

    }

}
