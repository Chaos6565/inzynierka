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

    private bool _complete = false;
    public bool Complete { get { return _complete; } }

    public void SetCompleted(bool state)
    {
        _complete = state;
    }

    private void Start()
    {
        // Jak ktoœ umie dodaæ to tu jest regex dla sprawdzenia: return\sn\s*\*\s*Silnia\(\s*n\s*-\s*1\s*\);
        results.Add("return n * Silnia(n-1);");
        results.Add("return n * Silnia(n - 1);");
        results.Add("return n*Silnia(n-1);");
        results.Add("return n*Silnia(n - 1);");
        results.Add("return n *Silnia(n - 1);");
        results.Add("return n* Silnia(n - 1);");
        results.Add("return n*Silnia(n- 1);");
        results.Add("return n*Silnia(n -1);");
        results.Add("return n *Silnia(n- 1);");
        results.Add("return n *Silnia(n -1);");
        results.Add("return n* Silnia(n- 1);");
        results.Add("return n* Silnia(n -1);");
        results.Add("g");
    }

    public void OpenTask()
    {
        taskCanvas.SetActive(true);
        _complete = false;
        taskText.text = "";
        GameStateManager.instance.programmingActive = true;
    }

    public void SubmitClick()
    {
        foreach (string s in results)
        {
            if (s == taskText.text)
            {
                _complete = true;
                background.SetActive(true);
                resultPanel.SetActive(true);
                resultPanelText.text = "Uda³o ci siê, zaliczy³eœ!";
                break;
            }
        }

        if (!Complete)
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

        GameStateManager.instance.programmingActive = false;
        if (Complete)
        { 
            Destroy(taskCanvas);
            Destroy(gameObject);
        }
    }
}
