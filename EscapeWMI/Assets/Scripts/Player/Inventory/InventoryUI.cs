using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviourPun
{

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform itemDisplay;


    public List<Sprite> itemDisplayList;
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
        itemDisplayList.Add((new Item { itemType = Item.ItemType.Ulotka }).GetDisplay());            // ULOTKA           index 0
        itemDisplayList.Add((new Item { itemType = Item.ItemType.Tablica }).GetDisplay());           // TABLICA          index 1
        itemDisplayList.Add((new Item { itemType = Item.ItemType.AnalizaNotatki }).GetDisplay());    // ANALIZA_NOTATKI  index 2
        itemDisplayList.Add((new Item { itemType = Item.ItemType.Analiza }).GetDisplay());           // ANALIZA          index 3
        itemDisplayList.Add((new Item { itemType = Item.ItemType.Algebra }).GetDisplay());           // ALGEBRA          index 4
        itemDisplayList.Add((new Item { itemType = Item.ItemType.Statystyka }).GetDisplay());        // STATYSTYKA       index 5
        itemDisplayList.Add((new Item { itemType = Item.ItemType.Grafy }).GetDisplay());             // GRAFY            index 6
        itemDisplayList.Add((new Item { itemType = Item.ItemType.Matematyka }).GetDisplay());        // MATEMATYKA       index 7
        itemDisplayList.Add((new Item { itemType = Item.ItemType.Fibonacci }).GetDisplay());         // FIBONACCI        index 8
    }


    public void ShareNote(Image note)
    {
        int itemIndex = 0;
        foreach (Sprite itemDisplay in itemDisplayList) {
            if (itemDisplay == note.sprite) break;
            else itemIndex++;  
        }
        photonView.RPC("RpcShareNote", RpcTarget.Others, itemIndex);
    }

    [PunRPC]
    public void RpcShareNote(int itemIndex)
    {
        Debug.Log("RPCShareNote");
        GameObject sharedItemDisplay = Instantiate(sharedItemDisplayPrefab, GameObject.Find("Shared Item Display Canvas").transform.position, Quaternion.identity, GameObject.Find("Shared Item Display Canvas").transform);
        sharedItemDisplay.gameObject.SetActive(true);
        sharedItemDisplay.transform.Find("Note").GetComponent<Image>().sprite = itemDisplayList[itemIndex];   
    }

}
