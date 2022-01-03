using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnglishManager : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject background;
    public GameObject resultPanel;

    public Text answer1;
    public Text answer2;
    public Text answer3;

    public Text resultPanelText;


    private bool _complete = false;
    public bool Complete { get { return _complete; } }

    public void OpenTask()
    {
        taskCanvas.SetActive(true);
        _complete = false;
        answer1.text = "";
        answer2.text = "";
        answer3.text = "";
        GameStateManager.instance.networkActive = true;
    }

    public void SubmitClick()
    {

        if (answer1.text == "Do trzech razy sztuka" || answer2.text == "Pierwsze koty za p³oty" || answer3.text == "Coœ jest nie tak")
        {
            _complete = true;
            background.SetActive(true);
            resultPanel.SetActive(true);
            resultPanelText.text = "Uda³o ci siê, zaliczy³eœ!";
        }

        if (!Complete)
        {
            background.SetActive(true);
            resultPanel.SetActive(true);
            resultPanelText.text = "Niestety, nie uda³o ci siê";
        }
    }

    public void CloseClick()
    {
        background.SetActive(false);
        resultPanel.SetActive(false);

        taskCanvas.SetActive(false);

        GameStateManager.instance.networkActive = false;
        if (Complete)
        {
            Destroy(taskCanvas);
            Destroy(gameObject);
        }
    }
}
