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
            SpawnItemWorld(1);
            SpawnItemWorld(2);
            SpawnItemWorld(3);
            SpawnItemWorld(4);
            SpawnItemWorld(5);
            SpawnItemWorld(6);
            SpawnItemWorld(7);
            SpawnItemWorld(8);

        }
    }

    public void InitializeItemsList()
    {
        // Add items here
        itemsList.Add(new Item { itemType = Item.ItemType.Ulotka });            // ULOTKA           index 0
        itemsList.Add(new Item { itemType = Item.ItemType.Tablica });           // TABLICA          index 1
        itemsList.Add(new Item { itemType = Item.ItemType.AnalizaNotatki });    // ANALIZA_NOTATKI  index 2
        itemsList.Add(new Item { itemType = Item.ItemType.Analiza });           // ANALIZA          index 3
        itemsList.Add(new Item { itemType = Item.ItemType.Algebra });           // ALGEBRA          index 4
        itemsList.Add(new Item { itemType = Item.ItemType.Statystyka });        // STATYSTYKA       index 5
        itemsList.Add(new Item { itemType = Item.ItemType.Grafy });             // GRAFY            index 6
        itemsList.Add(new Item { itemType = Item.ItemType.Matematyka });        // MATEMATYKA       index 7
        itemsList.Add(new Item { itemType = Item.ItemType.Fibonacci });         // FIBONACCI        index 8
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
