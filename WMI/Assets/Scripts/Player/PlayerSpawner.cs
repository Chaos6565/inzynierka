using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WMI
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab = null;
        [SerializeField] public GameObject[] spawnPoints = null;

        CameraScript cameraScript;

        int numberPlayers = 0;

        // Start is called before the first frame update
        private void Start()
        {
            cameraScript = FindObjectOfType<CameraScript>();

            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[DeterminePlayerSpawnPoint()].transform.position, Quaternion.identity);
            cameraScript.SetCameraTarget(player.transform);
        }

        private int DeterminePlayerSpawnPoint()
        {
            numberPlayers = PhotonNetwork.CountOfPlayers;

            return numberPlayers - 1;
        }
    }
}