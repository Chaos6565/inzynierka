using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


public class FormBottomHandler : MonoBehaviourPun
{
    [Header("Okna formularza")]
    [SerializeField] private GameObject allForm;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject resultWindowPass;
    [SerializeField] private GameObject resultWindowFailed;

    [Header("Wyniki formularza")]
    [SerializeField] private Toggle checkYes;
    [SerializeField] private Toggle checkNo;
    [SerializeField] private Text nameFromField;
    [SerializeField] private Text dropDownMenu;

    [Header("Zmienne do okna rezultatu")]
    [SerializeField] private Animator _animation = null;
    [SerializeField] private GameObject door = null;
    [SerializeField] private GameObject trigger = null;

    private bool check;
    private string nameInput;
    private string option;
    public static int successCounter = 0;
    byte COMPLETE_FORM_EVENT = 1;


    // Update is called once per frame
    void Update()
    {
        if (checkYes.GetComponent<Toggle>().isOn == true && checkNo.GetComponent<Toggle>().isOn == false)
        {
            check = true;
        }
        else
        {
            check = false;
        }

        nameInput = nameFromField.text;

        option = dropDownMenu.text;
        
    }



    public void ClickEndingButton()
    {

        if (check == true && nameInput != "" && option == "Opcja trzecia - ta do wygrania")
        {

            //photonView.RPC("AddToSuccessCounter", RpcTarget.All);
            //if (photonView.IsMine)
            //{
                CompleteForm();
            //}
            background.SetActive(true);
            resultWindowPass.SetActive(true);
            

        }
        else
        {
            background.SetActive(true);
            resultWindowFailed.SetActive(true);
        }
    }

    public void PassClick()
    {
        if (_animation.GetBool("isOpen") == false)
        {
            if (successCounter >= PhotonNetwork.CurrentRoom.PlayerCount)
            {
                Debug.Log($"Number of Players: {PhotonNetwork.CurrentRoom.PlayerCount}");
                Debug.Log($"Success Counter: {successCounter}");
                photonView.RPC("OpenTheDoor", RpcTarget.All);
            }
            resultWindowPass.SetActive(false);
            background.SetActive(false);
            allForm.SetActive(false);
        }
    }


    public void FailedClick()
    {
        resultWindowFailed.SetActive(false);
        background.SetActive(false);
        allForm.SetActive(false);
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(ExitGames.Client.Photon.EventData obj)
    {
        if (obj.Code == COMPLETE_FORM_EVENT)
        {
            AddToSuccessCounter();
            Debug.LogError("Received Event");
        }
    }

    private void CompleteForm()
    {
        Debug.LogError("Form Completed");
        Photon.Realtime.RaiseEventOptions raiseEventOptions = new Photon.Realtime.RaiseEventOptions { Receivers = Photon.Realtime.ReceiverGroup.Others };
        ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { };
        PhotonNetwork.RaiseEvent(COMPLETE_FORM_EVENT, new object[] { }, raiseEventOptions, sendOptions);
        AddToSuccessCounter();
        //photonView.RPC("AddToSuccessCounter", RpcTarget.All);
    }

    //[PunRPC]
    public void AddToSuccessCounter()
    {
        successCounter += 1;
        Debug.LogError($"Add Success! Current value: {successCounter}");
    }

    [PunRPC]
    void OpenTheDoor()
    {
        _animation.SetBool("isOpen", true);
        door.GetComponent<BoxCollider2D>().enabled = false;
        trigger.SetActive(false);
    }


}

