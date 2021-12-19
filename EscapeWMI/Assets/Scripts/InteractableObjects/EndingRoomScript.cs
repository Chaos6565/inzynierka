using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WMI
{
    public class EndingRoomScript : MonoBehaviourPun, IPunObservable
    {
        // Game objects
        [SerializeField] GameObject endgamePopup;
        [SerializeField] GameObject door;
        [SerializeField] GameObject NPC;

        int playersInTheRoom = 0;
        bool interacted = false;

        void Update()
        {
            if (!interacted)
            {
                if (door.gameObject.GetComponent<DoorScript>().IsTheDoorOpen() && playersInTheRoom >= PhotonNetwork.CurrentRoom.PlayerCount)
                {
                    NPC.gameObject.GetComponent<InteractableObject>().EnableInteractionForAll();
                    StartCoroutine(CloseTheDoorCoroutine());
                    interacted = true;
                }
            }
        }

        IEnumerator CloseTheDoorCoroutine()
        {
            yield return new WaitForSeconds(1);
            door.gameObject.GetComponent<DoorScript>().CloseTheDoor();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (!interacted)
            {
                if (stream.IsWriting)
                {
                    // We own this player: send the others our data
                    stream.SendNext(playersInTheRoom);
                    stream.SendNext(interacted);
                }
                else
                {
                    // Network player, receive data
                    this.playersInTheRoom = (int)stream.ReceiveNext();
                    this.interacted = (bool)stream.ReceiveNext();
                    Debug.Log($"Players In The Room: {playersInTheRoom}");
                }
            }
        }

        public void CloseThePopup()
        {
            endgamePopup.SetActive(false);
        }

        public void ExitTheGame()
        {
            Application.Quit();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player Has Entered The Room");
                playersInTheRoom++;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player Has Left The Room");
                playersInTheRoom--;
            }
        }

    }
}