using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class LectureTrigger : InteractableObject
{
    public string name;
    public string subject;
    public int ToDisable;
    public LectureManager Manager;
    public bool EndModule;
    [TextArea(3, 30)]
    public List<string> slides;

    [SerializeField] GameObject TriggerArea = null;
    private TriggerArea triggerArea = null;
    [SerializeField] List<Collider2D> playerColliders = null;
    [SerializeField] bool displayToEveryoneInsideRoom = false;
    [SerializeField] bool waitForEveryone = false;



    private bool isCompleted = false;

    private void Update()
    {
        if (EndModule == true)
        {
            if (!isCompleted && Manager.Complete == true)
            {
                isCompleted = true;
                GetComponentInParent<ModuleContentScript>().ModuleCompleted();
            }
        }
    }

    public override void PerformAction()
    {
        if (TriggerArea != null)
        {
            triggerArea = TriggerArea.GetComponent<TriggerArea>();
            Debug.Log("Num of players inside room: " + triggerArea.playersInsideRoomNumber);
        }
        if (!waitForEveryone)
        {
            DisplayLecture();
        }
        else
        {
            if (triggerArea.IsEveryoneInside)
            {
                Debug.Log("Everyone inside, starting lecture!");
                DisplayLecture();
            }
            else
            {
                // Wyswietl komunikat o koniecznosci oczekiwania na reszte graczy
            }
        }
    }

    private void DisplayLecture()
    {
        if (displayToEveryoneInsideRoom)
        {
            //photonView.RPC("RPCTriggerLecture", RpcTarget.All);

            if (TriggerArea != null)
            {
                playerColliders = triggerArea.playerColliders;
                Debug.Log("TARGET AREA IS NOT NULL");
            }
                
            if (TriggerArea != null && playerColliders != null)
            {
                Debug.Log("playerColliders len is: " + playerColliders.Count);
                foreach (Collider2D player in playerColliders)
                {
                    Debug.Log("Sent RPC at: " + player.GetComponent<PhotonView>().ViewID);


                    Photon.Realtime.Player targetedPlayer = player.GetComponent<PhotonView>().Owner;
                    photonView.RPC("RPCTriggerLecture", targetedPlayer);
                }
            }
            else
            {
                Debug.LogError("Couldn't obtain list of players in specified area.");
            }
        }
        else
        {
            RPCTriggerLecture();
        }
    }

    [PunRPC]
    public void RPCTriggerLecture()
    {
        switch (ToDisable)
        {
            case 1:
                this.EnableIntaraction();
                break;
            case 2:
                this.DisableInteraction();
                break;
            case 3:
                this.EnableInteractionForAll();
                break;
            case 4:
                this.DisableInteractionForAll();
                break;
        }
        Manager.StartLecture(slides, name, subject);
    }
}
