using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        ItemWorld.SpawnItemWorld(new Vector3(-115, 25), new Item { itemType = Item.ItemType.Ulotka });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
