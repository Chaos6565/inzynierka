using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemWorldSpawner : MonoBehaviourPun
{
    [SerializeField] public GameObject itemWorldPrefab;
    [SerializeField] public GameObject[] itemSpawnPoints = null;
    List<Item> itemsList = new List<Item>();
    List<ItemWorld> itemsWorldList = new List<ItemWorld>();

    void Start()
    {
        InitializeItemsList();

    }

    public void InitializeItemsList()
    {
        // Add items here
        itemsList.Add(new Item { itemType = Item.ItemType.Ulotka });            // ULOTKA               index 0
        itemsList.Add(new Item { itemType = Item.ItemType.Tablica });           // TABLICA              index 1
        itemsList.Add(new Item { itemType = Item.ItemType.AnalizaNotatki });    // ANALIZA NOTATKI      index 2
        itemsList.Add(new Item { itemType = Item.ItemType.Analiza });           // ANALIZA              index 3
        itemsList.Add(new Item { itemType = Item.ItemType.Algebra });           // ALGEBRA              index 4
        itemsList.Add(new Item { itemType = Item.ItemType.Statystyka });        // STATYSTYKA           index 5
        itemsList.Add(new Item { itemType = Item.ItemType.Grafy });             // GRAFY                index 6
        itemsList.Add(new Item { itemType = Item.ItemType.Matematyka });        // MATEMATYKA           index 7
        itemsList.Add(new Item { itemType = Item.ItemType.Fibonacci });         // FIBONACCI            index 8
        itemsList.Add(new Item { itemType = Item.ItemType.Plan2 });             // PLAN 2               index 9
        itemsList.Add(new Item { itemType = Item.ItemType.LogikaNotatki });     // LOGIKA NOTATKI       index 10
        itemsList.Add(new Item { itemType = Item.ItemType.TautologieA });       // TAUTOLOGIE A         index 11
        itemsList.Add(new Item { itemType = Item.ItemType.TautologieB });       // TAUTOLOGIE B         index 12
        itemsList.Add(new Item { itemType = Item.ItemType.GolebiaSmietanka });  // GOLEBIA SMIETANKA    index 13
        itemsList.Add(new Item { itemType = Item.ItemType.Podanie });           // PODANIE              index 14
        itemsList.Add(new Item { itemType = Item.ItemType.Haslo });             // HASLO                index 15
        itemsList.Add(new Item { itemType = Item.ItemType.Plan3 });             // PLAN 3               index 16

    }

    public void SpawnItemWorld(int itemIndex)
    {
        GameObject itemWorldGM = Instantiate(itemWorldPrefab, itemSpawnPoints[itemIndex].transform.position, Quaternion.identity);
        itemWorldGM.GetComponent<PhotonView>().ViewID = 700 + itemIndex;
        ItemWorld itemWorld = itemWorldGM.transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(itemsList[itemIndex]);
        itemsWorldList.Add(itemWorld);
        Debug.Log("Itemki zespawnowane");
        //photonView.RPC("SpawnItemWorldRPC", RpcTarget.All, itemIndex);
    }

    public void DestroyAllItemsWorld()
    {
        foreach (ItemWorld itemWorld in itemsWorldList)
        {
            if (itemWorld != null) { itemWorld.DestroySelf(); }
        }
        itemsWorldList.Clear();
        Debug.Log("Itemki zniszczono");
    }

    //[PunRPC]
    //public void SpawnItemWorldRPC(int itemIndex)
    //{

    //    GameObject itemWorldGM = Instantiate(itemWorldPrefab, itemSpawnPoints[itemIndex].transform.position, Quaternion.identity);
    //    itemWorldGM.GetComponent<PhotonView>().ViewID = 700 + itemIndex;
    //    ItemWorld itemWorld = itemWorldGM.transform.GetComponent<ItemWorld>();
    //    itemWorld.SetItem(itemsList[itemIndex]);
    //}
}
