using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Text EndButtonText;
    public Text EndButtonTextNPC;
    public GameObject NextButton;
    public GameObject EndBut;
    public GameObject EndButNPC;
    public Animator animator;
    bool IsNPC;

    private Queue<string> sentences;

    public GameObject dialogCanvas;

    private bool _complete = false;
    public bool Complete { get { return _complete; } }

    public void SetCompleted(bool state)
    {
        _complete = state;
    }


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        IsNPC = dialogue.IsNPC;
        dialogCanvas.SetActive(true);
        NextButton.SetActive(true);
        EndBut.SetActive(false);
        EndButNPC.SetActive(false);
        _complete = false;
        animator.SetBool("IsOpen", true);

        EndButtonText.text = dialogue.EndButtonText;
        EndButtonTextNPC.text = dialogue.EndButtonText;
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
            if(IsNPC== true)
            {
                EndButNPC.SetActive(true);
            }
            else
            {
                EndBut.SetActive(true);
            }  
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
        _complete = true;
        GameStateManager.instance.dialogActive = false;
    }
  
}
