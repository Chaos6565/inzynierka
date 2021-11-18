using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDialogueTrigger : MonoBehaviour
{
    public ChoiceDialogue dialogue;

    bool triggerEnter = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggerEnter == true)
        {
            TriggerDialogue();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        triggerEnter = true;
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<ChoiceDialogueManager>().StartDialogue(dialogue);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        triggerEnter = false;
    }

}
