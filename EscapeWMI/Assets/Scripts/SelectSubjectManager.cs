using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSubjectManager : MonoBehaviour
{
    [Header("Second year")]
    public GameObject chooseCanvasSecondYear;
    public Toggle firstSecondYear;
    public Toggle secondSecondYear;
    public Text warningSecondYear;

    [Header("Third year")]
    public GameObject chooseCanvasThirdYear;
    public Toggle firstThirdYear;
    public Toggle secondThirdYear;
    public Text warningThirdYear;


    private bool _complete = false;
    public bool Complete { get { return _complete; } }

    private int _choice = -1;
    public int Choice { get { return _choice; } }

    public void OpenTask(int year)
    {
        if(year == 2)
            chooseCanvasSecondYear.SetActive(true);
        else if(year == 3)
            chooseCanvasThirdYear.SetActive(true);

        _complete = false;
        GameStateManager.instance.taskActive = true;
    }



    public void SubmitForSecondYear()
    {
        if (firstSecondYear.isOn && secondSecondYear.isOn)
            warningSecondYear.text = "Nie mo¿na wybraæ obu";
        else if (!firstSecondYear.isOn && !secondSecondYear.isOn)
            warningSecondYear.text = "Trzeba wybraæ przedmiot";
        else if (firstSecondYear.isOn && !secondSecondYear.isOn)
        {
            chooseCanvasSecondYear.SetActive(false);
            _choice = 9;
            _complete = true;
            GameStateManager.instance.taskActive = false;
        }
        else if (secondSecondYear.isOn && !firstSecondYear.isOn)
        {
            chooseCanvasSecondYear.SetActive(false);

            _choice = 8;
            _complete = true;
            GameStateManager.instance.taskActive = false;
        }
    }

    public void SubmitFoThirdYear()
    {
        if (firstThirdYear.isOn && secondThirdYear.isOn)
            warningThirdYear.text = "Nie mo¿na wybraæ obu";
        else if (!firstThirdYear.isOn && !secondThirdYear.isOn)
            warningThirdYear.text = "Trzeba wybraæ przedmiot";
        else if (firstThirdYear.isOn && !secondThirdYear.isOn)
        {
            chooseCanvasThirdYear.SetActive(false);

            _choice = 17;
            _complete = true;
            GameStateManager.instance.taskActive = false;
        }
        else if (secondThirdYear.isOn && !firstThirdYear.isOn)
        {
            chooseCanvasThirdYear.SetActive(false);

            _choice = 16;
            _complete = true;
            GameStateManager.instance.taskActive = false;
        }
    }
}
