using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace WMI
{
    public class PlayerController : MonoBehaviourPun
    {
        Animator animation2d;
        Rigidbody2D rigidbody2d;
        SpriteRenderer spriteRen;

        public static PlayerController localPlayer;

        // Movement parameters
        [SerializeField] float runSpeed = 7;
        Vector2 movementVelocity;

        // List of all interactable objects in player's vicinity
        List<InteractableObject> interactableObjects;


        // Start is called before the first frame update - yes
        void Start()
        {
            
            if (photonView.IsMine)
            {
                localPlayer = this;
            }
            animation2d = GetComponent<Animator>();
            rigidbody2d = GetComponent<Rigidbody2D>();
            spriteRen = GetComponent<SpriteRenderer>();
            interactableObjects = new List<InteractableObject>();

            if (!photonView.IsMine)
            {
                Destroy(GetComponent<PlayerController>());
                return;
            }
        }

        private void Update()
        {
            TakeInput();
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        private void TakeInput()
        {
            if (photonView.IsMine)
            {
                // Action Key Managment
                if (!(interactableObjects.Count == 0))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        Debug.Log("Action Key Down");
                        InteractableObject nearestInteractable = interactableObjects[GetNearestInteractable()];
                        if (nearestInteractable.IsEnabled())
                        {
                            nearestInteractable.PerformAction();
                        }
                    }
                }

                // Movement Keys Managment
                movementVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            }
        }

        private int GetNearestInteractable()
        {
            int index = 0;
            float min = Vector3.Distance(transform.position, interactableObjects[0].transform.position);
            
            foreach (InteractableObject interactableObject in interactableObjects)
            {
                if (min > Vector3.Distance(transform.position, interactableObject.transform.position))
                {
                    min = Vector3.Distance(transform.position, interactableObject.transform.position);
                    index = interactableObjects.IndexOf(interactableObject);
                }
            }
            return index;
        }

        private void MovePlayer()
        {
            if (photonView.IsMine)
            {
                rigidbody2d.velocity = movementVelocity * runSpeed;

                animation2d.SetFloat("MoveX", rigidbody2d.velocity.x);
                animation2d.SetFloat("MoveY", rigidbody2d.velocity.y);

                if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1
                    || Input.GetAxisRaw("Vertical") == -1)
                {
                    animation2d.SetFloat("lastX", Input.GetAxisRaw("Horizontal"));
                    animation2d.SetFloat("lastY", Input.GetAxisRaw("Vertical"));
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (photonView.IsMine)
            {
                if (other.CompareTag("Interactable"))
                {
                    Debug.Log("Entered Interactable");
                    InteractableObject interactableObject = other.gameObject.GetComponent<InteractableObject>();
                    interactableObjects.Add(interactableObject);
                    Debug.Log($"Interactable is enabled? : {interactableObject.IsEnabled()}");
                }
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (photonView.IsMine)
            {
                if (other.CompareTag("Interactable"))
                {
                    Debug.Log("Left Interactable");
                    InteractableObject interactableObject = other.gameObject.GetComponent<InteractableObject>();
                    interactableObjects.Remove(interactableObject);
                }
            }
        }

    }
}