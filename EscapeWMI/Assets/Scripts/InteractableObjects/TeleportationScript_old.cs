using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationScript_old : MonoBehaviour
{
    //public GameObject teleportationObject;
    public Transform destiny;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = destiny.transform.position;
    }
}
