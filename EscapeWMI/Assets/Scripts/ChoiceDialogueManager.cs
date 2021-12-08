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
    private Queue<string> Choices;

    public GameObject dialogCanvas;
    public GameObject ButtonChoice1;
    public GameObject ButtonChoice2;
    public GameObject EndButton;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        Choices = new Queue<string>();
        ButtonChoice1.SetActive(false);
        ButtonChoice2.SetActive(false);
        EndButton.SetActive(false);
    }

    public void StartDialogue(ChoiceDialogue dialogue)
    {
        dialogCanvas.SetActive(true);


        nameText.text = dialogue.name;
        Choice2.text = dialogue.Buttonchoice2;

        sentences.Clear();
        Choices.Clear();

        foreach (string choice in dialogue.Buttonchoice1)
        {
            Choices.Enqueue(choice);
        }
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
  
        ButtonChoice1.SetActive(true);
        ButtonChoice2.SetActive(true);
        EndButton.SetActive(false);
    }

    public void ChoiceNext()
    {
        string choice;
        if (Choices.Count != 0)
        {
            choice = Choices.Dequeue();
            Choice1.text = choice;
            ButtonChoice1.SetActive(true);
            ButtonChoice2.SetActive(true);
            EndButton.SetActive(false);
        }
        else
        {
            ButtonChoice1.SetActive(false);
            ButtonChoice2.SetActive(false);
            EndButton.SetActive(true);
        }
    }
   

 
    public void DisplayNextSentence()
    {
        string sentence = sentences.Dequeue();
        ChoiceNext();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

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
