using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
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
        GameStateManager.instance.taskActive = true;
    }

    public void SubmitClick()
    {

        if (answer1.text == "sort studenci.txt" || answer2.text == "cat studenci.txt | tr 'a-z' 'A-Z'" || answer3.text == "wc -w studenci.txt")
        {
            _complete = true;
            background.SetActive(true);
            resultPanel.SetActive(true);
            resultPanelText.text = "Uda�o ci si�, zaliczy�e�!";
        }

        if (!Complete)
        {
            background.SetActive(true);
            resultPanel.SetActive(true);
            resultPanelText.text = "Niestety, nie uda�o ci si�";
        }
    }

    public void CloseClick()
    {
        background.SetActive(false);
        resultPanel.SetActive(false);

        taskCanvas.SetActive(false);

        GameStateManager.instance.taskActive = false;
        if (Complete)
        {
            Destroy(taskCanvas);
            Destroy(gameObject);
        }
    }
}
