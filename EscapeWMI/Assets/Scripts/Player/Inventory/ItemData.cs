using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{

    public static ItemData Instance 
    {
        private set;
        get; 
    }


    private void Awake()
    {
        Instance = this;
    }

    public Sprite ulotkaSprite;
    public Sprite ulotkaDisplay;
    public Sprite tablicaSprite;
    public Sprite tablicaDisplay;
    public Sprite analizaNotatkiSprite;
    public Sprite analizaNotatkiDisplay;
    public Sprite analizaSprite;
    public Sprite analizaDisplay;
    public Sprite algebraSprite;
    public Sprite algebraDisplay;
    public Sprite statystykaSprite;
    public Sprite statystykaDisplay;
    public Sprite grafySprite;
    public Sprite grafyDisplay;
    public Sprite matematykaSprite;
    public Sprite matematykaDisplay;
    public Sprite fibonacciSprite;
    public Sprite fibonacciDisplay;
    public Sprite plan2Sprite;
    public Sprite plan2Display;
    public Sprite logikaNotatkiSprite;
    public Sprite logikaNotatkiDisplay;
    public Sprite tautologieASprite;
    public Sprite tautologieADisplay;
    public Sprite tautologieBSprite;
    public Sprite tautologieBDisplay;
    public Sprite golebiaSmietankaSprite;
    public Sprite golebiaSmietankaDisplay;
    public Sprite podanieSprite;
    public Sprite podanieDisplay;
    public Sprite hasloSprite;
    public Sprite hasloDisplay;
    public Sprite plan3Sprite;
    public Sprite plan3Display;



}