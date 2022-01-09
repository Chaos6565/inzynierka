using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketingTrigger : InteractableObject
{
    public Button trigger;

    public bool EndModule;
    public MarketingManager Manager;
    private bool isCompleted = false;

    void Start()
    {
        Button btn = trigger.GetComponent<Button>();
        btn.onClick.AddListener(TriggerMarketing);
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

    public void TriggerMarketing()
    {

        Manager.OpenTask();
    }
}
