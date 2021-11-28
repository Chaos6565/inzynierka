using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PEManager : MonoBehaviour
{
    public GameObject PETask;

    public GameObject succedPanel;
    public GameObject failedPanel;
    public GameObject background;
    public Text resultText;


    public void OpenTask()
    {
        PETask.SetActive(true);
    }

    public void Result(bool r)
    {
        background.SetActive(true);

        if (r == true)
            succedPanel.SetActive(true);
        else
            failedPanel.SetActive(true);
            
    }

    public void ResultButtonClick()
    {
        background.SetActive(false);
        succedPanel.SetActive(false);
        failedPanel.SetActive(false);
        PETask.SetActive(false);
    }

    public void ReplayButtonClick()
    {
        background.SetActive(false);
        succedPanel.SetActive(false);
        failedPanel.SetActive(false);
    }
}
