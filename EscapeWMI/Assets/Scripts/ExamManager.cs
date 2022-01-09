using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamManager : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject background;
    public GameObject resultPanel;

    public Text resultPanelText;

    public Toggle answer1a;
    public Toggle answer1b;
    public Toggle answer1c;

    public Toggle answer2a;
    public Toggle answer2b;
    public Toggle answer2c;

    public Toggle answer3a;
    public Toggle answer3b;
    public Toggle answer3c;

    int points;

    private bool _complete = false;
    public bool Complete { get { return _complete; } }
    
    public void OpenTask()
    {
        taskCanvas.SetActive(true);
        points = 0;

        _complete = false;
        GameStateManager.instance.taskActive = true;
    }

    public void SubmitClick()
    {
        if (!answer1a.isOn && !answer1b.isOn && answer1c.isOn) points++;

        if (!answer2a.isOn && answer2b.isOn && !answer2c.isOn) points++;

        if (answer1a.isOn && !answer1b.isOn && !answer1c.isOn) points++;

        if(points >= 2)
        {
            background.SetActive(true);
            resultPanel.SetActive(true);
            resultPanelText.text = "Uda³o ci siê, zaliczy³eœ!";

            _complete = true;
        }
        else
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

        GameStateManager.instance.taskActive = false;
        if (Complete)
        {
            Destroy(taskCanvas);
            Destroy(gameObject);
        }
    }

}
