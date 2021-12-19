using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PEManager : MonoBehaviour
{
    public GameObject PETask;

    public GameObject succedPanel;
    public GameObject failedPanel;
    public GameObject background;
    public GameObject Ball;
    public Text resultText;
    public bool Complete;

    private void Reset()
    {
    }
    public void OpenTask()
    {
        PETask.SetActive(true);
        
        Complete = false;
        GameStateManager.instance.PEActive = true;
    }

    public void Result(bool r)
    {
        background.SetActive(true);

        if (r == true)
        {
            succedPanel.SetActive(true);
        }
        else
        {
            failedPanel.SetActive(true);
        }
            
            
    }

    public void ResultButtonClick()
    {
        background.SetActive(false);
        succedPanel.SetActive(false);
        failedPanel.SetActive(false);
        PETask.SetActive(false);
        Complete = true;

        GameStateManager.instance.PEActive = false;
    }

    public void ReplayButtonClick()
    {
        background.SetActive(false);
        succedPanel.SetActive(false);
        failedPanel.SetActive(false);
        PETask.SetActive(false);
        Complete = false;
        
        GameStateManager.instance.PEActive = false;
        OpenTask();
    }
}
