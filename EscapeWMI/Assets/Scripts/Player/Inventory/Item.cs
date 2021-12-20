using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{

    public enum ItemType
    {
        Ulotka,
        Item2,
        Item3,
    }

    public ItemType itemType;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Ulotka: return ItemData.Instance.ulotkaSprite;
            case ItemType.Item2: return ItemData.Instance.item2Sprite;
            case ItemType.Item3: return ItemData.Instance.item3Sprite;
        }
    }

    public Sprite GetDisplay()
    {
        switch (itemType)
        {
            default:
            case ItemType.Ulotka: return ItemData.Instance.ulotkaDisplay;
            case ItemType.Item2: return ItemData.Instance.item2Sprite;
            case ItemType.Item3: return ItemData.Instance.item3Sprite;
        }
    }

}