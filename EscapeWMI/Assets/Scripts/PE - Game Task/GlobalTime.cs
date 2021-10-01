using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalTime : MonoBehaviour
{

    public GameObject timeDisplay;
    public int seconds = 10;
    public bool deductingTime;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deductingTime == false)
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
