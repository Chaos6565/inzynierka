using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlgebraExamTrigger : InteractableObject
{
    public Button trigger;

    public bool EndModule;
    public AlgebraExamManager Manager;
    private bool isCompleted = false;

    void Start()
    {
        Button btn = trigger.GetComponent<Button>();
        btn.onClick.AddListener(TriggerAlgebraExam);
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

    public void TriggerAlgebraExam()
    {

        Manager.OpenTask();
    }
}
