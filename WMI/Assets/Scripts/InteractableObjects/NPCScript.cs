using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


namespace WMI
{
    public class NPCScript : InteractableObject
    {
        // Game objects
        [SerializeField] GameObject door;
        [SerializeField] GameObject endgamePopup;


        private void Start()
        {
            this.DisableInteraction();
        }

        public override void PerformAction()
        {
            photonView.RPC("ShowPopup", RpcTarget.All);
            door.gameObject.GetComponent<DoorScript>().OpenTheDoor();
            this.DisableInteraction();
        }

        [PunRPC]
        void ShowPopup()
        {
            endgamePopup.SetActive(true);
        }

    }
}