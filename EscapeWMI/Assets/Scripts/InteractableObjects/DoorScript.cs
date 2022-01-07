using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DoorScript : InteractableObject
{
    // Components
    [SerializeField] List<GameObject> doors = null;

    // Parameters
    bool isClosed = true;
    [SerializeField] bool operatedManually = false;

    public override void PerformAction()
    {
        if (operatedManually)
        {
            if (isClosed)
                OpenTheDoor();
            else
                CloseTheDoor();
        }
    }

    public void OpenTheDoor()
    {
        photonView.RPC("OpenTheDoorRPC", RpcTarget.All);
    }

    public void CloseTheDoor()
    {
        photonView.RPC("CloseTheDoorRPC", RpcTarget.All);
    }

    [PunRPC]
    void OpenTheDoorRPC()
    {
        gameObject.GetComponent<Animator>().SetBool("isOpen", true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        SetClosedState(false);

        if (doors != null)
        {
            foreach (GameObject door in doors)
            {
                if (door.GetComponent<DoorScript>().IsTheDoorClosed())
                {
                    door.GetComponent<Animator>().SetBool("isOpen", true);
                    door.GetComponent<BoxCollider2D>().enabled = false;
                    door.GetComponent<DoorScript>().SetClosedState(false);
                }
            }
        }
    }

    [PunRPC]
    void CloseTheDoorRPC()
    {
        gameObject.GetComponent<Animator>().SetBool("isOpen", false);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        SetClosedState(true);

        if (doors != null)
        {
            foreach (GameObject door in doors)
            {
                if (door.GetComponent<DoorScript>().IsTheDoorClosed())
                {
                    door.GetComponent<Animator>().SetBool("isOpen", false);
                    door.GetComponent<BoxCollider2D>().enabled = true;
                    door.GetComponent<DoorScript>().SetClosedState(true);
                }
                
            }
        }
    }

    public void SetClosedState(bool state)
    {
        isClosed = state;
    }

    public bool IsTheDoorClosed()
    {
        return isClosed;
    }

    public void SetManualOperation(bool mode)
    {
        photonView.RPC("RPCSetManualOperation", RpcTarget.All, mode);
    }

    [PunRPC]
    public void RPCSetManualOperation(bool mode)
    {
        operatedManually = mode;
    }

}