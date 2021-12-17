using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{

    public enum ItemType
    {
        Item1,
        Item2,
        Item3,
    }

    public ItemType itemType;


    public Sprite GetSprite()
    {
        return itemType switch
        {
            ItemType.Item2 => ItemData.Instance.item2Sprite,
            ItemType.Item3 => ItemData.Instance.item3Sprite,
            _ => ItemData.Instance.item1Sprite,
        };
    }

}