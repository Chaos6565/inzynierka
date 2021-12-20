using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemWorldSpawner : MonoBehaviourPun
{
    [SerializeField] public GameObject itemWorldPrefab;
    [SerializeField] public GameObject[] itemSpawnPoints = null;
    List<Item> itemsList = new List<Item>();

    void Start()
    {
        InitializeItemsList();

        if (PhotonNetwork.IsMasterClient)
        {
            /*for (int i = 0; i < itemsList.Count; i++)
            {
                SpawnItemWorld(i);
                Debug.Log("Position: " + itemSpawnPoints[i].transform.position);
            }*/
            SpawnItemWorld(0);
            Debug.Log("Position: " + itemSpawnPoints[0].transform.position);
        }
    }

    public void InitializeItemsList()
    {
        // Add items here
        itemsList.Add(new Item { itemType = Item.ItemType.Ulotka }); // ULOTKA index 0
    }

    public void SpawnItemWorld(int itemIndex)
    {
        photonView.RPC("SpawnItemWorldRPC", RpcTarget.All, itemIndex);
    }

    [PunRPC]
    public void SpawnItemWorldRPC(int itemIndex)
    {

        GameObject itemWorldGM = Instantiate(itemWorldPrefab, itemSpawnPoints[itemIndex].transform.position, Quaternion.identity);
        itemWorldGM.GetComponent<PhotonView>().ViewID = 700 + itemIndex;
        ItemWorld itemWorld = itemWorldGM.transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(itemsList[itemIndex]);
    }
}
