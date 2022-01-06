using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;
using System;

public class InventoryUI : MonoBehaviourPun
{

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform itemDisplay;


    public List<Item> itemList;
    public PlayerController player;
    
    [SerializeField] GameObject sharedItemDisplayPrefab;


    public void Init()
    {

        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");

        itemSlotTemplate.gameObject.SetActive(false);

        photonView.ViewID = 600;

        InitializeItemsList();

    }

    public void CreateInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        itemSlotTemplate.gameObject.SetActive(true);

        foreach (Transform child in itemSlotContainer.transform)
        {
            if (child == itemSlotTemplate)
            {
                continue;
            }

            else
            {
                Destroy(child.gameObject);
            }

        }

        Debug.Log("Item quantity: " + inventory.GetItemList().Count);
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 60f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            itemDisplay = itemSlotRectTransform.Find("ItemDisplay");
            itemDisplay.gameObject.SetActive(true);
            itemDisplay.gameObject.SetActive(false);
            Image note = itemDisplay.Find("Note").GetComponent<Image>();
            note.sprite = item.GetDisplay();

            x++;
            if (x >= 4)
            {
                y += 1;
                x = 0;
            }
        }

        itemSlotTemplate.gameObject.SetActive(false);

    }


    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }


    public void InitializeItemsList()
    {
        // Add items here
        itemList.Add(new Item { itemType = Item.ItemType.Ulotka });            // ULOTKA           index 0
        itemList.Add(new Item { itemType = Item.ItemType.Tablica });           // TABLICA          index 1
        itemList.Add(new Item { itemType = Item.ItemType.AnalizaNotatki });    // ANALIZA_NOTATKI  index 2
        itemList.Add(new Item { itemType = Item.ItemType.Analiza });           // ANALIZA          index 3
        itemList.Add(new Item { itemType = Item.ItemType.Algebra });           // ALGEBRA          index 4
        itemList.Add(new Item { itemType = Item.ItemType.Statystyka });        // STATYSTYKA       index 5
        itemList.Add(new Item { itemType = Item.ItemType.Grafy });             // GRAFY            index 6
        itemList.Add(new Item { itemType = Item.ItemType.Matematyka });        // MATEMATYKA       index 7
        itemList.Add(new Item { itemType = Item.ItemType.Fibonacci });         // FIBONACCI        index 8
        itemList.Add(new Item { itemType = Item.ItemType.Plan2 });             // PLAN2            index 9
    }


    public void DisplayItem(GameObject itemDisplayObject)
    {
        if (this.player.itemDisplayViewAvaliable)
        {
            itemDisplayObject.SetActive(true);
            this.player.itemDisplayViewAvaliable = false;
        }
    }


    public void HideItem(GameObject itemDisplayObject)
    {
        itemDisplayObject.SetActive(false);
        this.player.itemDisplayViewAvaliable = true;
    }


    public void DestroyNote(Image note)
    {

        foreach (Item item in inventory.GetItemList())
        {
            if (item.GetDisplay() == note.sprite)
            {
                inventory.RemoveItemFromList(item);
                break;
            }


        }
        
    }


    public void ShareNote(Image note)
    {
        int itemIndex = 0;
        foreach (Item item in itemList) {
            if (item.GetDisplay() == note.sprite) break;
            else itemIndex++;  
        }
        photonView.RPC("RpcShareNote", RpcTarget.Others, itemIndex, this.player.transform.position.x, this.player.transform.position.y);
    }

    [PunRPC]
    public void RpcShareNote(int itemIndex, float originX, float originY)
    { 
        float distanceToOriginX = Math.Abs(this.player.transform.position.x - originX);
        float distanceToOriginY = Math.Abs(this.player.transform.position.y - originY);
        if (distanceToOriginX <= 3.0 && distanceToOriginY <= 3.0 && this.player.itemDisplayViewAvaliable)
        {
            GameObject sharedItemDisplay = Instantiate(sharedItemDisplayPrefab, GameObject.Find("Shared Item Display Canvas").transform.position, Quaternion.identity, GameObject.Find("Shared Item Display Canvas").transform);
            sharedItemDisplay.gameObject.SetActive(true);
            sharedItemDisplay.transform.Find("Note").GetComponent<Image>().sprite = itemList[itemIndex].GetDisplay();
            sharedItemDisplay.GetComponent<sharedItemDisplay>().SetPlayer(this.player);
            this.player.itemDisplayViewAvaliable = false;
        }
    }

}
