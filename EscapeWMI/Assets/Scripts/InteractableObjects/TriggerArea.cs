using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TriggerArea : MonoBehaviourPun
{
    [SerializeField] public List<GameObject> doors = null;
    [SerializeField] public List<Collider2D> playerColliders = null;
    [SerializeField] public bool closeTheDoorWhenEveryoneInside = false;
    [SerializeField] public bool destroyAfterUse = false;
    public int playersInsideRoomNumber = 0;
    private bool everyoneInside = false;
    public bool IsEveryoneInside { get { return everyoneInside; } }

    private bool doorsOpen = true;

    private void Update()
    {
        //Debug.Log($"Players In The Room: {playersInsideRoomNumber}");

        if (PhotonNetwork.IsMasterClient)
        {
            if (playersInsideRoomNumber >= PhotonNetwork.CurrentRoom.PlayerCount)
            {
                if (!everyoneInside)
                {
                    everyoneInside = true;
                    UpdatePlayerInsideFlag(everyoneInside);

                    //gameObject.GetComponent<BoxCollider2D>().enabled = false;

                    if (closeTheDoorWhenEveryoneInside && doorsOpen && doors != null)
                    {
                        StartCoroutine(CloseTheDoorCoroutine());
                        doorsOpen = false;
                    }

                    if (destroyAfterUse)
                        PhotonNetwork.Destroy(gameObject);
                }
            }
            else
            {
                if (everyoneInside)
                {
                    everyoneInside = false;
                    UpdatePlayerInsideFlag(everyoneInside);

                    //gameObject.GetComponent<BoxCollider2D>().enabled = true;

                    if (!doorsOpen && doors != null)
                    {
                        StartCoroutine(OpenTheDoorCoroutine());
                        doorsOpen = true;
                    }

                }
            }
        }
    }

    IEnumerator CloseTheDoorCoroutine()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject door in doors)
        {
            DoorScript _doorScript = door.gameObject.GetComponent<DoorScript>();
            if (!_doorScript.IsTheDoorClosed())
            {
                _doorScript.CloseTheDoor();
                _doorScript.SetManualOperation(true);
            }
        }
    }

    IEnumerator OpenTheDoorCoroutine()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject door in doors)
        {
            DoorScript _doorScript = door.gameObject.GetComponent<DoorScript>();
            if (_doorScript.IsTheDoorClosed())
            {
                _doorScript.OpenTheDoor();
                _doorScript.SetManualOperation(false);
            }
        }
    }
 
    private void UpdatePlayerInsideFlag(bool flag)
    {
        if (PhotonNetwork.IsMasterClient)
            photonView.RPC("RPCUpdatePlayerInsideFlag", RpcTarget.Others, flag);
    }

    [PunRPC]
    private void RPCUpdatePlayerInsideFlag(bool flag)
    {
        everyoneInside = flag;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Has Entered The Room");
            if (PhotonNetwork.IsMasterClient)
                playersInsideRoomNumber++;
            playerColliders.Add(other);

            //Debug.LogError("Photon View ID of a player inside room: " + other.GetComponent<PhotonView>().ViewID);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Has Left The Room");
            if (PhotonNetwork.IsMasterClient)
                playersInsideRoomNumber--;
            playerColliders.Remove(other);
        }
    }
}
