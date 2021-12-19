using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Text EndButtonText;
    public GameObject NextButton;
    public GameObject EndBut;
    public bool End;
    public Animator animator;

    private Queue<string> sentences;

    public GameObject dialogCanvas;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        dialogCanvas.SetActive(true);
        NextButton.SetActive(true);
        EndBut.SetActive(false);
        End = false;
        animator.SetBool("IsOpen", true);

        EndButtonText.text = dialogue.EndButtonText;
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

        GameStateManager.instance.dialogActive = true;
    }



    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            NextButton.SetActive(false);
            EndBut.SetActive(true);
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
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
        animator.SetBool("IsOpen", false);
        End = true;
        GameStateManager.instance.dialogActive = false;
    }
  
}
