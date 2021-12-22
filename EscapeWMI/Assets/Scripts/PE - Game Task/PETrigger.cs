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
        if (EndModule == true)
        {
            if (!isCompleted && Manager.Complete == true)
            {
                isCompleted = true;
                GetComponentInParent<ModuleContentScript>().ModuleCompleted();
            }
        }
    }

    public void TriggerPE()
    {
       
        FindObjectOfType<PEManager>().OpenTask();
    }
}
