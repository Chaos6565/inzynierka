using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sharedItemDisplay : MonoBehaviour
{
    public PlayerController player;


    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }


    public void DestroySelf()
    {
        this.player.itemDisplayViewAvaliable = true;
        Destroy(this.gameObject);
    }
}
