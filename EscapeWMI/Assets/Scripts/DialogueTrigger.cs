using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : InteractableObject
{
    public Dialogue dialogue;

    public override void PerformAction()
    {
        TriggerDialogue();
        GetComponentInParent<ModuleContentScript>().ModuleCompleted();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
