using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDialogueTrigger : InteractableObject
{
    public ChoiceDialogue dialogue;
    public bool EndModule;
    public int ToDisable;
    public ChoiceDialogueManager Manager;
    private void Update()
    {
        if (EndModule == true)
        {
            if (Manager.Complete == true)
            {
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
        FindObjectOfType<ChoiceDialogueManager>().StartDialogue(dialogue);
    }

}
