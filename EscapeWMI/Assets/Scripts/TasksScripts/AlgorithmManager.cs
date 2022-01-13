using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlgorithmManager : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject background;
    public GameObject resultPanel;

    public Text resultPanelText;

    public Toggle answer1;
    public Toggle answer2;
    public Toggle answer3;
    public Toggle answer4;

    private bool _complete = false;
    public bool Complete { get { return _complete; } }

    public void SetCompleted(bool state)
    {
        _complete = state;
    }

    public void OpenTask()
    {
        taskCanvas.SetActive(true);

        _complete = false;
        GameStateManager.instance.taskActive = true;
    }

    public void SubmitClick()
    {
        if (!answer1.isOn && !answer2.isOn && !answer3.isOn && answer4.isOn)
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
