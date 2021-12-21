using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Photon.Pun;

public class InventoryUI : MonoBehaviourPun
{

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform itemDisplay;
    public Transform sharedItemDisplayNote;
    public Transform sharedItemDisplayCloseButton;
    public PlayerController player;

    public void Init()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        sharedItemDisplayNote = GameObject.Find("Shared Item Display Canvas").transform.Find("SharedItemDisplay").Find("Note");
        sharedItemDisplayCloseButton = GameObject.Find("Shared Item Display Canvas").transform.Find("SharedItemDisplay").Find("CloseButton");

        itemSlotTemplate.gameObject.SetActive(false);
        sharedItemDisplayNote.gameObject.SetActive(false);
        sharedItemDisplayCloseButton.gameObject.SetActive(false);

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
        float itemSlotCellSize = 50f;
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


    public void ShareNote(Image note)
    {
        photonView.RPC("RpcShareNote", RpcTarget.Others, note.sprite);
    }

    [PunRPC]
    public void RpcShareNote(Sprite sharedNote)
    {
        
        
        GameObject.Find("Shared Item Display Canvas").transform.Find("SharedItemDisplay").GetChild(0).GetComponent<Image>().sprite = sharedNote;    //GetChild(0) = sharedItemDisplayNote
        GameObject.Find("Shared Item Display Canvas").transform.Find("SharedItemDisplay").GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Shared Item Display Canvas").transform.Find("SharedItemDisplay").GetChild(1).gameObject.SetActive(true);                   //GetChild(1) = sharedItemDisplayCloseButton

    }

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }

}
