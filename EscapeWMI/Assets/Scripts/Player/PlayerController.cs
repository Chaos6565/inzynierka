using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class PlayerController : MonoBehaviourPun
{
    Animator animation2d;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRen;

    public static PlayerController localPlayer;

    // Movement parameters
    [SerializeField] float baseSpeed = 7;
    Vector2 movementVelocity;

    // TURBOOO For debbugggging
    [SerializeField] float turbo = 20;
    float runSpeed = 0;

    // List of all interactable objects in player's vicinity
    List<InteractableObject> interactableObjects;

    //player's inventory
    private Inventory inventory;
    [SerializeField] private InventoryUI inventoryUI;


    public bool canMove = true;

    // Start is called before the first frame update - yes
    void Start()
    {
        if (photonView.IsMine)
        {
            localPlayer = this;

            
            inventoryUI.SetPlayer(localPlayer);
            
        }
        animation2d = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        interactableObjects = new List<InteractableObject>();

        runSpeed = baseSpeed; // do turbo, mozna usunac potem


        inventory = new Inventory(UseInventoryItem);
        inventoryUI.CreateInventory(inventory);

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
            //turrbbo
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("TURBOOOOOOOOOOOOOOOOO");
                if(runSpeed < baseSpeed + turbo)
                {
                    runSpeed = baseSpeed + turbo;
                } 
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                runSpeed = baseSpeed;
            }
            // koniec turrrbobo
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
        if (photonView.IsMine && canMove == true)
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

    private void UseInventoryItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Item1:

                break;
            case Item.ItemType.Item2:

                break;
            case Item.ItemType.Item3:

                break;

        }
    }

}