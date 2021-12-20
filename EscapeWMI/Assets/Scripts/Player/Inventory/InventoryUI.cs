using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform itemDisplay;
    public PlayerController player;

    public void Init()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");

        itemSlotTemplate.gameObject.SetActive(false);

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

}
