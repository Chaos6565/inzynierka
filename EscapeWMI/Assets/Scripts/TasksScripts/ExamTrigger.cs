using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamTrigger : InteractableObject
{
    public Button trigger;
 
    public bool EndModule;
    public ExamManager Manager;
    private bool isCompleted = false;

    void Start()
    {
        Button btn = trigger.GetComponent<Button>();
        btn.onClick.AddListener(TriggerExam);
    }

    private void Update()
    {
        if (EndModule)
        {
            if (!isCompleted && Manager.Complete)
            {
                isCompleted = true;
                GetComponentInParent<ModuleContentScript>().ModuleCompleted();
                Manager.SetCompleted(false);
                Destroy(this);
            }
        }

    }
    
    public void TriggerExam()
    {
        
        Manager.OpenTask();
    }
}
