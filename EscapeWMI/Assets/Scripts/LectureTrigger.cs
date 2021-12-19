using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LectureTrigger : InteractableObject
{
    public string name;
    public string subject;
    public int ToDisable;
    public LectureManager Manager;
    public bool EndModule;

    [TextArea(3, 30)]
    public List<string> slides;


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
        TriggerLecture();
    }

    public void TriggerLecture()
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
        FindObjectOfType<LectureManager>().StartLecture(slides, name, subject);
    }
}
