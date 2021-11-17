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
        switch (itemType)
        {
            default:
            case ItemType.Item1: return ItemData.Instance.item1Sprite;
            case ItemType.Item2: return ItemData.Instance.item2Sprite;
            case ItemType.Item3: return ItemData.Instance.item3Sprite;
        }
    }

}