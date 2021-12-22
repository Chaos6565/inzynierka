using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingTrigger : MonoBehaviour
{
    public Button trigger;
 
    public bool EndModule;
    public ProgrammingScript Manager;

    private bool isCompleted = false;

    void Start()
    {
        Button btn = trigger.GetComponent<Button>();
        btn.onClick.AddListener(TriggerProgramming);
    }

    private void Update()
    {
        if (EndModule == true)
        {
            if (!isCompleted && Manager.Complete == true)
            {
                isCompleted = true;
                GetComponentInParent<ModuleContentScript>().ModuleCompleted();
                Debug.Log("PROGRAMMING MANAGER COMPLETED STATE: " + Manager.Complete);
            }
        }
    }

    public void TriggerProgramming()
    {
       
        FindObjectOfType<ProgrammingScript>().OpenTask();
    }
}
