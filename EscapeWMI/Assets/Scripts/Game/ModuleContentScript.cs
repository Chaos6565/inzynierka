using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using WMI;

public class ModuleContentScript : MonoBehaviourPun
{
    private bool _isCompleted = false;
    public bool IsCompleted { get { return _isCompleted; } }

    //public DoorScript doorScript;
    [SerializeField] public bool openDoorWhenCompleted = false;
    [SerializeField] public List<GameObject> doors = null;


    // Is completing a task by everyone necessary for whole module completion?
    [SerializeField]  private bool moduleCompletionByEveryone = false;

    private int completionCounter = 0;

    public void ModuleCompleted()
    {
        //doorScript.OpenTheDoor();
        if (openDoorWhenCompleted)
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<DoorScript>().OpenTheDoor();
            }
        }
        photonView.RPC("ModuleCompletedRPC", RpcTarget.All);
    }

    [PunRPC]
    public void ModuleCompletedRPC()
    {
        if (moduleCompletionByEveryone)
        {
            completionCounter++;
            if (completionCounter >= PhotonNetwork.CurrentRoom.PlayerCount)
            {
                _isCompleted = true;
            }
        }
        else
        {
            _isCompleted = true;
        }
    }

}
