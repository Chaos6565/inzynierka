using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Animator lowerAnimation = null;
    [SerializeField] private Animator leftAnimation = null;

    [SerializeField] private GameObject lowerDoor = null;
    [SerializeField] private GameObject leftDoor = null;

    [SerializeField] private GameObject trigger = null;


    private bool hasPlayer = false;

    private void Update()
    {
        if (hasPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            if (leftAnimation.GetBool("isOpen") == false)
            {
                leftAnimation.SetBool("isOpen", true);
                leftDoor.GetComponent<BoxCollider2D>().enabled = false;
                trigger.SetActive(false);
            }
            if (lowerAnimation.GetBool("isOpen") == false)
            {
                lowerAnimation.SetBool("isOpen", true);
                lowerDoor.GetComponent<BoxCollider2D>().enabled = false;
                trigger.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            hasPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = false;
        }
    }
}
