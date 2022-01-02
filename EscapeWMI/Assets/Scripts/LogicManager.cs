using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject background;
    public GameObject resultPanel;

    public Text resultPanelText;

    public Toggle answer1;
    public Toggle answer2;
    public Toggle answer3;
    public Toggle answer4;
    public Toggle answer5;

    int points;

    private bool _complete = false;
    public bool Complete { get { return _complete; } }

    public void OpenTask()
    {
        taskCanvas.SetActive(true);
        points = 0;

        _complete = false;
        GameStateManager.instance.logicActive = true;
    }

    public void SubmitClick()
    {
        if (answer1.isOn && !answer2.isOn && !answer3.isOn && answer4.isOn && answer5.isOn) points = 3;

        if ((answer1.isOn && !answer2.isOn && !answer3.isOn && !answer4.isOn && answer5.isOn) ||
            (answer1.isOn && !answer2.isOn && !answer3.isOn && answer4.isOn && !answer5.isOn) ||
            (!answer1.isOn && !answer2.isOn && !answer3.isOn && answer4.isOn && answer5.isOn)) points = 2;

        if ((answer1.isOn && !answer2.isOn && !answer3.isOn && !answer4.isOn && !answer5.isOn) ||
            (!answer1.isOn && !answer2.isOn && !answer3.isOn && !answer4.isOn && answer5.isOn) ||
            (!answer1.isOn && !answer2.isOn && !answer3.isOn && answer4.isOn && !answer5.isOn)) points = 1;

        if (points == 3)
        {
            background.SetActive(true);
            resultPanel.SetActive(true);
            resultPanelText.text = "Brawo otrzymuje Pan/Pani pi¹tkê";

            _complete = true;
        }
        else if(points == 2)
        {
            background.SetActive(true);
            resultPanel.SetActive(true);
            resultPanelText.text = "Brawo otrzymuje Pan/Pani czwórkê.";
        }
        else if (points == 1)
        {
            background.SetActive(true);
            resultPanel.SetActive(true);
            resultPanelText.text = "Brawo otrzymuje Pan/Pani trójkê.";
        }
        else
        {
            background.SetActive(true);
            resultPanel.SetActive(true);
            resultPanelText.text = "Niestety nie zaliczy³eœ testu";
        }

    }


    public void CloseClick()
    {
        background.SetActive(false);
        resultPanel.SetActive(false);

        taskCanvas.SetActive(false);

        GameStateManager.instance.logicActive = false;
        if (Complete)
        {
            Destroy(taskCanvas);
            Destroy(gameObject);
        }
    }

}
