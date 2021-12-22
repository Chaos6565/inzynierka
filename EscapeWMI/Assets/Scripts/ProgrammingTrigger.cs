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
        if (EndModule)
        {
            if (!isCompleted && Manager.Complete)
            {
                isCompleted = true;
                GetComponentInParent<ModuleContentScript>().ModuleCompleted();
                Destroy(this);
            }
        }
    }

    public void TriggerProgramming()
    {
        Manager.OpenTask();
    }
}
