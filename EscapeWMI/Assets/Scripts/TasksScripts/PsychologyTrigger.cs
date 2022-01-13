using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PsychologyTrigger : InteractableObject
{
    public Button trigger;

    public bool EndModule;
    public PsychologyManager Manager;

    private bool isCompleted = false;

    void Start()
    {
        Button btn = trigger.GetComponent<Button>();
        btn.onClick.AddListener(TriggerPsychology);
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

    public void TriggerPsychology()
    {
        Manager.OpenTask();
    }
}
