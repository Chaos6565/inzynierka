using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScriot : MonoBehaviour
{
    public GameObject menuCanvas;

    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject quitPanel;

    public bool isActive = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            mainPanel.SetActive(true);
            settingsPanel.SetActive(false);
            quitPanel.SetActive(false);

            menuCanvas.SetActive(!isActive);

            isActive = !isActive;

            GameStateManager.instance.menuActive = isActive;
        }
    }

    public void ButtonClick(string buttonName)
    {
        if (buttonName == "Game Button")
        {
            mainPanel.SetActive(true);
            settingsPanel.SetActive(false);
            quitPanel.SetActive(false);

            menuCanvas.SetActive(false);
            isActive = false;

            GameStateManager.instance.menuActive = false;
        }
        else if (buttonName == "Option Button")
        {
            mainPanel.SetActive(false);
            settingsPanel.SetActive(true);
            quitPanel.SetActive(false);
        }
        else if (buttonName == "Quit Button")
        {
            mainPanel.SetActive(true);
            settingsPanel.SetActive(false);
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
            quitPanel.SetActive(false);
        }

    }
    public void GoBack()
    {
        quitPanel.SetActive(false);
    }
}
