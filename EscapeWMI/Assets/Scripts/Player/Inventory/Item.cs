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
        Tablica,
        AnalizaNotatki,
        Analiza,
        Algebra,
        Statystyka,
        Grafy,
        Matematyka,
        Fibonacci,
}

    public ItemType itemType;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Ulotka:           return ItemData.Instance.ulotkaSprite;
            case ItemType.Tablica:          return ItemData.Instance.tablicaSprite;
            case ItemType.AnalizaNotatki:   return ItemData.Instance.analizaNotatkiSprite;
            case ItemType.Analiza:          return ItemData.Instance.analizaSprite;
            case ItemType.Algebra:          return ItemData.Instance.algebraSprite;
            case ItemType.Statystyka:       return ItemData.Instance.statystykaSprite;
            case ItemType.Grafy:            return ItemData.Instance.grafySprite;
            case ItemType.Matematyka:       return ItemData.Instance.matematykaSprite;
            case ItemType.Fibonacci:        return ItemData.Instance.fibonacciSprite;
        }
    }

    public Sprite GetDisplay()
    {
        switch (itemType)
        {
            default:
            case ItemType.Ulotka:           return ItemData.Instance.ulotkaDisplay;
            case ItemType.Tablica:          return ItemData.Instance.tablicaDisplay;
            case ItemType.AnalizaNotatki:   return ItemData.Instance.analizaNotatkiDisplay;
            case ItemType.Analiza:          return ItemData.Instance.analizaDisplay;
            case ItemType.Algebra:          return ItemData.Instance.algebraDisplay;
            case ItemType.Statystyka:       return ItemData.Instance.statystykaDisplay;
            case ItemType.Grafy:            return ItemData.Instance.grafyDisplay;
            case ItemType.Matematyka:       return ItemData.Instance.matematykaDisplay;
            case ItemType.Fibonacci:        return ItemData.Instance.fibonacciDisplay;
        }
    }

}