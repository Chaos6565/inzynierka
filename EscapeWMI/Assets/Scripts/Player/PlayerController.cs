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

    [SerializeField] public int playerNumber;

    // Movement parameters
    [SerializeField] float baseSpeed = 10;
    Vector2 movementVelocity;

    // TURBOOO For debbugggging
    [SerializeField] float turbo = 16;
    float runSpeed = 0;

    // List of all interactable objects in player's vicinity
    List<InteractableObject> interactableObjects;

    //player's inventory
    private Inventory inventory;
    private InventoryUI inventoryUI;
    private int inventoryLimit;
    public bool itemDisplayViewAvaliable;
    [SerializeField] private GameObject inventoryUIPrefab;


    public bool canMove = true;

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

        runSpeed = baseSpeed; // do turbo, mozna usunac potem

        inventory = new Inventory(UseInventoryItem);
        InitializeInventory();


        if (!photonView.IsMine)
        {
            Destroy(GetComponent<PlayerController>());
            return;
        }
    }

    private void InitializeInventory()
    {
        if (photonView.IsMine)
        {
            GameObject inventoryUIgameObject = Instantiate(inventoryUIPrefab, GameObject.Find("Inventory UI Canvas").transform.position, Quaternion.identity, GameObject.Find("Inventory UI Canvas").transform);
            inventoryUI = inventoryUIgameObject.GetComponent<InventoryUI>();
            inventoryUI.Init();
            inventoryUI.CreateInventory(inventory);
            inventoryUI.SetPlayer(localPlayer);
            itemDisplayViewAvaliable = true;
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

            // tp do obajtka
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Yo obajtek whats up");
                this.transform.position = new Vector3(0, -1, 0);
            }
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
            else if (other.CompareTag("ItemWorld"))
            {
                ItemWorld itemWorld = other.GetComponent<ItemWorld>();
                if (itemWorld != null)
                {
                    if (PhotonNetwork.CountOfPlayers < 4) { inventoryLimit = 3; }
                    else if (PhotonNetwork.CountOfPlayers < 8) { inventoryLimit = 2; }
                    else inventoryLimit = 1;
                    if (inventory.GetItemList().Count < inventoryLimit) {
                        inventory.AddItemToList(itemWorld.GetItem());
                        itemWorld.DestroySelf();
                    }
                    
                }
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
        //switch (item.itemType)
        //{
        //    case Item.ItemType.Ulotka:
        //        //GameObject itemDisplayGameObject = Instantiate(itemDisplayPrefab, GameObject.Find("ItemDisplayCanvas").transform.position, Quaternion.identity, GameObject.Find("ItemDisplayCanvas").transform);
        //        //itemDisplayGameObject.SetActive(true);
        //        break;
        //    case Item.ItemType.Item2:

        //        break;
        //    case Item.ItemType.Item3:

        //        break;

        //}
    }

}