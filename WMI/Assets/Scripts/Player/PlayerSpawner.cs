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
        [SerializeField] private GameObject[] playerPrefab = null;
        [SerializeField] public GameObject[] spawnPoints = null;

        CameraScript cameraScript;

        // Start is called before the first frame update
        private void Start()
        {
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
                GameObject player = PhotonNetwork.Instantiate(playerPrefab[PhotonNetwork.LocalPlayer.ActorNumber - 1].name, spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.position, Quaternion.identity);

                cameraScript.SetCameraTarget(player.transform);
            }
        }

    }
}