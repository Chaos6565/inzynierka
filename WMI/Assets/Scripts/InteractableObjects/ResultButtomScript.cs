using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButtomScript : MonoBehaviour
{
    [SerializeField] private GameObject result = null;
    [SerializeField] private Animator _animation = null;


    [SerializeField] private GameObject form = null;
    [SerializeField] private GameObject background = null;

    [SerializeField] private GameObject door = null;

    public void PassClick()
    {
        if (_animation.GetBool("isOpen") == false)
        {
            _animation.SetBool("isOpen", true);
            door.GetComponent<BoxCollider2D>().enabled = false;
            result.SetActive(false);
            background.SetActive(false);
            form.SetActive(false);
        }
    }

    public void FailedClick()
    {
        result.SetActive(false);
        background.SetActive(false);
        form.SetActive(false);
    }
}
