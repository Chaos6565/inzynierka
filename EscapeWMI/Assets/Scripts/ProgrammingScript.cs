using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingScript : MonoBehaviour
{
    public GameObject taskCanvas;
    public GameObject background;
    public GameObject resultPanel;

    public Text taskText;
    public Text resultPanelText;

    private List<string> results = new List<string>();
    bool check = false;

    private void Start()
    {
        results.Add("return n * Silnia(n-1);");
        results.Add("return n * Silnia(n - 1);");
        results.Add("return n*Silnia(n-1);");
        results.Add("return n*Silnia(n - 1);");
    }

    public void OpenTask()
    {
        taskCanvas.SetActive(true);
        taskText.text = "";
        check = false;
    }


    public void SubmitClick()
    {
        foreach(string s in results)
        {
            if (s == taskText.text)
            {
                background.SetActive(true);
                resultPanel.SetActive(true);
                resultPanelText.text = "Uda³o ci siê, zaliczy³eœ!";

                check = true;
            }
        }

        if (check == false)
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
    }
}
