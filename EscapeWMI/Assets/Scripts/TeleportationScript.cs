using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationScript : MonoBehaviour
{
    //public GameObject teleportationObject;
    public Transform destiny;

    void OnTriggerEnter2D(Collider2D other)
    {
        //other.transform.position = destiny.transform.position;
        other.GetComponent<Rigidbody2D>().position = destiny.transform.position;
    }
}
