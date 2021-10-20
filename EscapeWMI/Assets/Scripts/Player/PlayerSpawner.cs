using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

namespace WMI
{
    public class PlayerSpawner : MonoBehaviourPun
    {
        [SerializeField]
        private GameObject[] playerPrefab = null;
        [SerializeField]
        public GameObject[] spawnPoints = null;

        Player[] playersInRoom;
        int myNumberInRoom = 0;

        CameraScript cameraScript;

        private void Start()
        {
            playersInRoom = PhotonNetwork.PlayerList;

            foreach (Player p in playersInRoom)
            {
                if (p != PhotonNetwork.LocalPlayer)
                {
                    myNumberInRoom++;
                }
                else
                    break;
            }

            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                photonView.RPC("SpawnPlayers", RpcTarget.All);
            }
        }

        [PunRPC]
        void SpawnPlayers()
        {
            if (PhotonNetwork.LocalPlayer.IsLocal)
            {
                cameraScript = FindObjectOfType<CameraScript>();
                GameObject player = PhotonNetwork.Instantiate(playerPrefab[myNumberInRoom].name, spawnPoints[myNumberInRoom].transform.position, Quaternion.identity);

                cameraScript.SetCameraTarget(player.transform);
            }
        }

    }
}