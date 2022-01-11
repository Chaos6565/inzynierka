using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SelectSubjectTrigger : InteractableObject
{
    public int year;

    public int ToDisable;
    public SelectSubjectManager Manager;
    public bool EndModule;

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
            if (!isCompleted && Manager.Complete == true && Manager.Choice != -1)
            {
                isCompleted = true;
                GetComponentInParent<GameStateManager>().AddToSkipList(Manager.Choice);
                GetComponentInParent<ModuleContentScript>().ModuleCompleted();
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

        Manager.OpenTask(year);
    }
}
