using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ChoiceDialogueTrigger : InteractableObject
{
    public ChoiceDialogue dialogue;
    public bool EndModule;
    public int ToDisable;
    public ChoiceDialogueManager Manager;
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
                GetComponentInParent<ModuleContentScript>().ModuleCompleted();
                DisableInteraction();
                Manager.SetCompleted(false);
                Destroy(this);
            }
        }
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

    public void SetDisplayForEveryone(bool state)
    {
        displayToEveryoneInsideRoom = state;
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
                        photonView.RPC("RPCTriggerDialogue", targetedPlayer);
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
            RPCTriggerDialogue();
        }
    }

    [PunRPC]
    public void RPCTriggerDialogue()
    {
        switch (ToDisable)
        {
            case 0: break;
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
        Manager.StartDialogue(dialogue);
    }

}
