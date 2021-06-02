using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DoorScript : MonoBehaviourPun
{
    [SerializeField] private Animator _animation = null;

    [SerializeField] private GameObject door = null;

    private bool hasPlayer = false;

    public bool isTheDoorOpen = false;

    private void Update()
    {
        if (hasPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            if (_animation.GetBool("isOpen") == false)
            {
                this.photonView.RPC("OpenTheDoor", RpcTarget.All);
            }
            else
            {
                this.photonView.RPC("CloseTheDoor", RpcTarget.All);
            }
        }
    }

    /*
    public void DoorHinge()
    {
        Debug.Log("The Hinge Seems To Be Covered In Oil Of Some Sort");
        if (isTheDoorOpen == false)
        {
            this.photonView.RPC("OpenTheDoor", RpcTarget.All);
            Debug.Log("Door is closed");
            //isTheDoorOpen = true;
        }
        else
        {
            this.photonView.RPC("CloseTheDoor", RpcTarget.All);
            //isTheDoorOpen = false;
        }
    }*/

    [PunRPC]
    void OpenTheDoor()
    {
        _animation.SetBool("isOpen", true);
        door.GetComponent<BoxCollider2D>().enabled = false;
        isTheDoorOpen = true;
    }

    [PunRPC]
    void CloseTheDoor()
    {
        _animation.SetBool("isOpen", false);
        door.GetComponent<BoxCollider2D>().enabled = true;
        isTheDoorOpen = false;
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
