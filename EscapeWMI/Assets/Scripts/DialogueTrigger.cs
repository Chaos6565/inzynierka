using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : InteractableObject
{
    public Dialogue dialogue;
    public DialogueManager Manager;
    public bool EndModule;
    public int ToDisable;

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
        TriggerDialogue();
        
    }

    public void TriggerDialogue()
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
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
