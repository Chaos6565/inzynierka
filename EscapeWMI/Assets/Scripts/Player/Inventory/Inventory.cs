using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public event EventHandler OnItemListChanged;

    private List<Item> itemList;
    private Action<Item> useItemAction;

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void AddItemToList(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItemFromList(Item item)
    {
        itemList.Remove(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public Inventory(Action<Item> useItemAction)
    {
        itemList = new List<Item>();

        AddItemToList(new Item { itemType = Item.ItemType.Item1 });
        AddItemToList(new Item { itemType = Item.ItemType.Item2 });
        AddItemToList(new Item { itemType = Item.ItemType.Item3 });

        this.useItemAction = useItemAction;
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }



}
