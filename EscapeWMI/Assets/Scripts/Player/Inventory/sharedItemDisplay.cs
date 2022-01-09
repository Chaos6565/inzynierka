using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedItemDisplay : MonoBehaviour
{
    public PlayerController player;
    public InventoryUI inventoryUI;


    public void SetPlayer(PlayerController player, InventoryUI inventoryUI)
    {
        this.player = player;
        this.inventoryUI = inventoryUI;
    }


    public void DestroySelf()
    {
        this.player.itemDisplayViewAvaliable = true;
        inventoryUI.player.itemDisplayViewAvaliable = true;
        Destroy(this.gameObject);
    }
}
