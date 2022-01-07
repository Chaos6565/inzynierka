using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TriggerArea : MonoBehaviourPun, IPunObservable
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
        if (playersInsideRoomNumber >= PhotonNetwork.CurrentRoom.PlayerCount)
        {
            everyoneInside = true;
        }
        else
        {
            everyoneInside = false;
        }

        if (everyoneInside)
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (closeTheDoorWhenEveryoneInside && doorsOpen && doors != null)
            {
                StartCoroutine(CloseTheDoorCoroutine());
                //closeTheDoorWhenEveryoneInside = false;
                doorsOpen = false;
            }

            if (destroyAfterUse)
                Destroy(gameObject);
        }
        else
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;

            if (!doorsOpen && doors != null)
            {
                StartCoroutine(OpenTheDoorCoroutine());
                doorsOpen = true;
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (!everyoneInside)
        //{
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(playersInsideRoomNumber);
            }
            else
            {
                // Network player, receive data
                this.playersInsideRoomNumber = (int)stream.ReceiveNext();

                Debug.Log($"Players In The Room: {playersInsideRoomNumber}");
            }
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Has Entered The Room");
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
            playersInsideRoomNumber--;
            playerColliders.Remove(other);
        }
    }
}
