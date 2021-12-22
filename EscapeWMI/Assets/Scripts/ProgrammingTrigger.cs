using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingTrigger : MonoBehaviour
{
    public Button trigger;
 
    public bool EndModule;
    public ProgrammingScript Manager;

    void Start()
    {
        Button btn = trigger.GetComponent<Button>();
        btn.onClick.AddListener(TriggerProgramming);
    }

    private void Update()
    {
        if (EndModule == true)
        {
            if (Manager.Complete == true)
            {
                GetComponentInParent<ModuleContentScript>().ModuleCompleted();
            }
        }

    }
    
    public void TriggerProgramming()
    {
       
        FindObjectOfType<ProgrammingScript>().OpenTask();
    }
}
