using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamTrigger : InteractableObject
{
    public Button trigger;
    public bool ActivateOnClick;
    public bool EndModule;
    public ExamManager Manager;
    void Start()
    {
        Button btn = trigger.GetComponent<Button>();
        btn.onClick.AddListener(TriggerExam);
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
    public override void PerformAction()
    {
        if (ActivateOnClick == true)
        {
            TriggerExam();

        }

    }
    public void TriggerExam()
    {
        FindObjectOfType<ExamManager>().OpenTask();
    }
}
