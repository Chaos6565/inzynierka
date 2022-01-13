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
    public GameObject notification;

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
                DisableInteraction();
                Manager.SetCompleted(false);
                GetComponentInParent<ModuleContentScript>().ModuleCompleted();
                //Destroy(this);
            }
        }
        else
        {
            if (!isCompleted && Manager.Complete == true)
                Manager.SetCompleted(false);
        }
    }

    public void SetDisplayForEveryone(bool state)
    {
        displayToEveryoneInsideRoom = state;
    }

    public override void PerformAction()
    {
        if (TriggerArea != null)
        {
            triggerArea = TriggerArea.GetComponent<TriggerArea>();
        }
        if (!waitForEveryone)
        {
            TriggerTask();
        }
        else
        {
            if (TriggerArea != null)
            {
                if (triggerArea.IsEveryoneInside)
                {
                    Debug.Log("Everyone inside, starting lecture!");
                    TriggerTask();
                }
                else
                {
                    notification.SetActive(true);
                    // Wyswietl komunikat o koniecznosci oczekiwania na reszte graczy
                }
            }
            else
            {
                Debug.LogError("Trigger area not found.");
            }
        }
    }

    private void TriggerTask()
    {
        if (displayToEveryoneInsideRoom)
        {
            if (TriggerArea != null)
            {
                playerColliders = triggerArea.playerColliders;

                if (playerColliders != null)
                {
                    foreach (Collider2D player in playerColliders)
                    {
                        Photon.Realtime.Player targetedPlayer = player.GetComponent<PhotonView>().Owner;
                        photonView.RPC("RPCTriggerLecture", targetedPlayer);
                    }
                }
                else
                {
                    Debug.LogError("Couldn't obtain list of players in specified area.");
                }
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
        SetDisplayForEveryone(false);
        Manager.StartLecture(slides, name, subject);
    }
}
