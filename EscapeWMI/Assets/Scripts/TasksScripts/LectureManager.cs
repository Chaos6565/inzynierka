using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LectureManager : MonoBehaviour
{
    public Text profName;
    public Text subject;
    public Text slide;

    List<string> slides;

    public GameObject lectureCanvas;

    int counter = 0;

    private bool _complete = false;
    public bool Complete { get { return _complete; } }

    public void SetCompleted(bool state)
    {
        _complete = state;
    }

    public void StartLecture(List<string> slides_, string name, string subject_)
    {
        lectureCanvas.SetActive(true);
        slides = slides_;
        slide.text = slides_[counter];
        profName.text = name;
        subject.text = subject_;
        _complete = false;

        GameStateManager.instance.lectureActive = true;
    }

    public void ClickForward()
    {
        if (counter == slides.Count - 1)
            EndLecture();
        else
        {
            counter++;
            slide.text = slides[counter];
        }
    }

    public void EndLecture()
    {
        counter = 0;
        lectureCanvas.SetActive(false);
        _complete = true;
        GameStateManager.instance.lectureActive = false;
    }
}
