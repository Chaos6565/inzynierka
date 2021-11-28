using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PETrigger : InteractableObject
{
    public override void PerformAction()
    {
        TriggerPE();
        GetComponentInParent<ModuleContentScript>().ModuleCompleted();
    }

    public void TriggerPE()
    {
        FindObjectOfType<PEManager>().OpenTask();
    }
}
