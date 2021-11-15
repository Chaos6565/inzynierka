using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LectureTrigger : InteractableObject
{
    public string name;
    public string subject;

    [TextArea(3, 30)]
    public List<string> slides;

    public override void PerformAction()
    {
        TriggerLecture();
        GetComponentInParent<ModuleContentScript>().ModuleCompleted();
    }

    public void TriggerLecture()
    {
        FindObjectOfType<LectureManager>().StartLecture(slides, name, subject);
    }
}
