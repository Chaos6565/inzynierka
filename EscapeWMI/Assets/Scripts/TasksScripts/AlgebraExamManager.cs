using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlgebraExamManager : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject background;
    public GameObject resultPanel;

    public Text resultPanelText;

    public Toggle answer1_1;
    public Toggle answer1_2;
    public Toggle answer1_3;

    public Toggle answer2_1;
    public Toggle answer2_2;
    public Toggle answer2_3;

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
        if (answer1_1.isOn && !answer1_2.isOn && !answer1_3.isOn) points++;

        if (!answer2_1.isOn && answer2_2.isOn && !answer2_3.isOn) points++;

        if (points >= 1)
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
