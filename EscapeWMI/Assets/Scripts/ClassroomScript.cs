using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassroomScript : MonoBehaviour
{
    public GameObject classroomCanvas;
    public Text classNumber;

    public string number;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            classroomCanvas.SetActive(true);

            classNumber.text = number;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        classroomCanvas.SetActive(false);
    }
}
