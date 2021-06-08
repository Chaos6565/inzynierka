using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WMI
{
    public class EndingRoomScript : MonoBehaviourPun
    {
        [SerializeField] Collider2D roomCollider;
        [SerializeField] Collider2D NPCCollider;
        [SerializeField] GameObject door;

        int playersInTheRoom = 0;

        void Update()
        {
            if (playersInTheRoom == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                //door.gameObject.
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playersInTheRoom++;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playersInTheRoom--;
            }
        }
    }
}