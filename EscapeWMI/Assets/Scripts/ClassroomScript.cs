using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ClassroomScript : MonoBehaviourPun
{
    public GameObject classroomCanvas;
    public Text classNumber;

    public string number;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<PhotonView>().IsMine)
        {
            classroomCanvas.SetActive(true);

            classNumber.text = number;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PhotonView>().IsMine)
            classroomCanvas.SetActive(false);
    }
}
