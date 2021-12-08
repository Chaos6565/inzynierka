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
        
        if (sentences.Count == 1)
        {
            EndButton.SetActive(true);
            ButtonChoice1.SetActive(false);
            ButtonChoice2.SetActive(false);
            return;
        }
        string sentence = sentences.Dequeue();
        string choice = Choices.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        StartCoroutine(TypeChoice(choice));
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
    IEnumerator TypeChoice(string choice)
    {
        Choice1.text = "";
        foreach (char letter in choice.ToCharArray())
        {
            Choice1.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        dialogCanvas.SetActive(false);
        EndButton.SetActive(false);
    }

}
