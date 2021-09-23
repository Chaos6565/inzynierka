using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject quitPanel;

    public void ButtonClick(string buttonName)
    {
        if (buttonName == "Game Button")
        {
            mainPanel.SetActive(true);
            settingsPanel.SetActive(false);
            creditsPanel.SetActive(false);
            quitPanel.SetActive(false);
        }
        if (buttonName == "Option Button")
        {
            mainPanel.SetActive(false);
            settingsPanel.SetActive(true);
            creditsPanel.SetActive(false);
            quitPanel.SetActive(false);
        }
        if (buttonName == "Credits Button")
        {
            mainPanel.SetActive(false);
            settingsPanel.SetActive(false);
            creditsPanel.SetActive(true);
            quitPanel.SetActive(false);
        }
        if(buttonName == "Quit Button")
        {
            mainPanel.SetActive(false);
            settingsPanel.SetActive(false);
            creditsPanel.SetActive(false);
            quitPanel.SetActive(true);
        }
    }

    public void QuitButtons(bool state)
    {
        if (state == true)
            Application.Quit();
        else
        {
            mainPanel.SetActive(true);
            settingsPanel.SetActive(false);
            creditsPanel.SetActive(false);
            quitPanel.SetActive(true);
        }

    }
}
