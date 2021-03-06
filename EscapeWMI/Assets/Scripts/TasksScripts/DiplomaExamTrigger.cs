using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiplomaExamTrigger : InteractableObject
{
    public Button trigger;

    public bool EndModule;
    public DiplomaExamManager Manager;
    private bool isCompleted = false;

    void Start()
    {
        Button btn = trigger.GetComponent<Button>();
        btn.onClick.AddListener(TriggerDiplomaExam);
    }

    private void Update()
    {
        if (EndModule)
        {
            if (!isCompleted && Manager.Complete)
            {
                isCompleted = true;
                DisableInteraction();
                Manager.SetCompleted(false);
                GetComponentInParent<ModuleContentScript>().ModuleCompleted();
            }
        }

    }

    public void TriggerDiplomaExam()
    {

        Manager.OpenTask();
    }
}
