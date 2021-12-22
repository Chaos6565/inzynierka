using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingTrigger : InteractableObject
{
    public Button trigger;
    public bool ActivateOnClick;
    public bool EndModule;
    public ProgrammingScript Manager;
    public int ToDisable;
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
    public override void PerformAction()
    {
        if (ActivateOnClick == true)
        {
            TriggerProgramming();

        }

    }
    public void TriggerProgramming()
    {
        switch (ToDisable)
        {
            case 1:
                this.EnableIntaraction();
                break;
            case 2:
                this.DisableInteraction();
                break;
            case 3:
                this.EnableInteractionForAll();
                break;
            case 4:
                this.DisableInteractionForAll();
                break;
        }
        FindObjectOfType<ProgrammingScript>().OpenTask();
    }
}
