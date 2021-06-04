using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Projekt.PlayerSpawner
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab = null;
        CameraScript cameraScript;

        // Start is called before the first frame update
        private void Start()
        {
            SpawnPlayers();
        }

        private void SpawnPlayers()
        {
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            cameraScript = FindObjectOfType<CameraScript>();
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(-1.18f, -12.81f, 0), Quaternion.identity);
            cameraScript.SetCameraTarget(player.transform);
        }
    }
}

