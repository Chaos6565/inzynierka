using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalTime : MonoBehaviour
{
    public GameObject timeDisplay;
    public int seconds = 10;
    public bool deductingTime;

    public GameObject PETask;
    public GameObject succedPanel;
    public GameObject failedPanel;
    

    // Update is called once per frame
    void Update()
    {
        if (deductingTime == false && PETask.activeSelf == true && succedPanel.activeSelf == false && failedPanel.activeSelf == false)
        {
            deductingTime = true;
            StartCoroutine(DeductSecond());
        }
    }

    IEnumerator DeductSecond()
    {
        yield return new WaitForSeconds(1);
        seconds -= 1;
        timeDisplay.GetComponent<Text>().text = "Czas: " + seconds;
        deductingTime = false;
    }
}
