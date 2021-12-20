using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WMI
{
    public class DoorScript : InteractableObject
    {
        // Components
        public GameObject[] doors;

        // Parameters
        bool isOpen = false;
        [SerializeField] bool operatedManually = false;



        public override void PerformAction()
        {
            if (operatedManually)
            {
                if (!isOpen)
                    OpenTheDoor();
                else
                    CloseTheDoor();
            }
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
                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].GetComponent<Animator>().SetBool("isOpen", true);
                    doors[i].GetComponent<BoxCollider2D>().enabled = false;
                    isOpen = true;
                }
            }
        }

        [PunRPC]
        void CloseTheDoorRPC()
        {
            if (isOpen)
            {
                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].GetComponent<Animator>().SetBool("isOpen", false);
                    doors[i].GetComponent<BoxCollider2D>().enabled = true;
                    isOpen = false;
                }
            }
        }

        public bool IsTheDoorOpen()
        {
            return isOpen;
        }

    }
}