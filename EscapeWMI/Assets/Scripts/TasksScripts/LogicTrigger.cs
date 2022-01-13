using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicTrigger : MonoBehaviour
{
    public Button trigger;

    public bool EndModule;
    public LogicManager Manager;
    private bool isCompleted = false;

    void Start()
    {
        Button btn = trigger.GetComponent<Button>();
        btn.onClick.AddListener(TriggerLogic);
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

    public void TriggerLogic()
    {

        Manager.OpenTask();
    }
}
