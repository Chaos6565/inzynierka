using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DoorScript : MonoBehaviourPun
{
    [SerializeField] private Animator lowerAnimation = null;
    [SerializeField] private Animator leftAnimation = null;

    [SerializeField] private GameObject lowerDoor = null;
    [SerializeField] private GameObject leftDoor = null;

    [SerializeField] private GameObject trigger = null;


    private bool hasPlayer = false;

    private void Update()
    {
        if (hasPlayer && Input.GetKeyDown(KeyCode.F))
        {
            if (leftAnimation.GetBool("isOpen") == false)
            {
                photonView.RPC("OpenLeftDoor", RpcTarget.All);
            }
            if (lowerAnimation.GetBool("isOpen") == false)
            {
                photonView.RPC("OpenLowerDoor", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void OpenLowerDoor()
    {
        lowerAnimation.SetBool("isOpen", true);
        lowerDoor.GetComponent<BoxCollider2D>().enabled = false;
        trigger.SetActive(false);
    }
    [PunRPC]
    void OpenLeftDoor()
    {
        leftAnimation.SetBool("isOpen", true);
        leftDoor.GetComponent<BoxCollider2D>().enabled = false;
        trigger.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            hasPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = false;
        }
    }
}
