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
        FindObjectOfType<PEManager>().OpenTask();
    }
}
