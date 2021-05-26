using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Animator _animation = null;

    [SerializeField] private GameObject door = null;

    private bool hasPlayer = false;

    private void Update()
    {
        if (hasPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            if (_animation.GetBool("isOpen") == false)
            {
                _animation.SetBool("isOpen", true);
                door.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                _animation.SetBool("isOpen", false);
                door.GetComponent<BoxCollider2D>().enabled = true;
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
