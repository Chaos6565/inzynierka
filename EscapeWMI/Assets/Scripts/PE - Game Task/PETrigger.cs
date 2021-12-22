using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PETrigger : InteractableObject
{
    public Button trigger;
    public bool ActivateOnClick;
    public bool EndModule;
    public PEManager Manager;
    public int ToDisable;
    void Start()
    {
        Button btn = trigger.GetComponent<Button>();
        btn.onClick.AddListener(TriggerPE);
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
            TriggerPE();

        }

    }
    public void TriggerPE()
    {
        switch (ToDisable)
        {
            case 0: break;
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
        FindObjectOfType<PEManager>().OpenTask();
    }
}
