using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PETrigger : MonoBehaviour
{
    public Button trigger;

    public bool EndModule;
    public PEManager Manager;

    private bool isCompleted = false;

    void Start()
    {
        Button btn = trigger.GetComponent<Button>();
        btn.onClick.AddListener(TriggerPE);
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

    public void TriggerPE()
    {
        Manager.OpenTask();
    }
}
