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
            GetComponentInParent<GameStateManager>().AddToSkipList(9);
            chooseCanvasSecondYear.SetActive(false);

            _complete = true;
        }
        else if (secondSecondYear.isOn && !firstSecondYear.isOn)
        {
            gameObject.GetComponentInParent<GameStateManager>().AddToSkipList(8);
            chooseCanvasSecondYear.SetActive(false);

            _complete = true;
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
            GetComponentInParent<GameStateManager>().AddToSkipList(17);
            chooseCanvasThirdYear.SetActive(false);

            _complete = true;
        }
        else if (secondThirdYear.isOn && !firstThirdYear.isOn)
        {
            GetComponentInParent<GameStateManager>().AddToSkipList(16);
            chooseCanvasThirdYear.SetActive(false);

            _complete = true;
        }
    }
}
