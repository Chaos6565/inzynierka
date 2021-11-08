using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LectureTrigger : MonoBehaviour
{
    public string name;
    public string subject;

    [TextArea(3, 30)]
    public List<string> slides;

    bool triggerEnter = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggerEnter == true)
        {
            TriggerLecture();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        triggerEnter = true;
    }

    public void TriggerLecture()
    {
        FindObjectOfType<LectureManager>().StartLecture(slides, name, subject);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        triggerEnter = false;
    }
}
