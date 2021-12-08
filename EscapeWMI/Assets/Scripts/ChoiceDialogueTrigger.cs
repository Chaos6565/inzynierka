using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDialogueTrigger : InteractableObject
{
    public ChoiceDialogue dialogue;

    public override void PerformAction()
    {
        TriggerDialogue();
        GetComponentInParent<ModuleContentScript>().ModuleCompleted();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<ChoiceDialogueManager>().StartDialogue(dialogue);
    }

}
