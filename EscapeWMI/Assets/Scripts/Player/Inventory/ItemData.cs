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

    public Sprite item1Sprite;
    public Sprite item2Sprite;
    public Sprite item3Sprite;

}