using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceDialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Text Choice1;
    public Text Choice2;


    private Queue<string> sentences;

    public GameObject dialogCanvas;
    public GameObject ButtonChoice1;
    public GameObject ButtonChoice2;
    public GameObject EndButton;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        ButtonChoice1.SetActive(false);
        ButtonChoice2.SetActive(false);
        EndButton.SetActive(false);
    }

    public void StartDialogue(ChoiceDialogue dialogue)
    {
        dialogCanvas.SetActive(true);


        nameText.text = dialogue.name;
        Choice1.text = dialogue.Buttonchoice1;
        Choice2.text = dialogue.Buttonchoice2;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
        MakeaChoice();
        EndButton.SetActive(false);
    }


    void MakeaChoice()
    {

        ButtonChoice1.SetActive(true);
        ButtonChoice2.SetActive(true);
        
    }
    public void DisplayNextSentence()
    {
        ButtonChoice1.SetActive(false);
        ButtonChoice2.SetActive(false);
        
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        EndButton.SetActive(true);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        dialogCanvas.SetActive(false);
        EndButton.SetActive(false);
    }

}
