using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace WMI
{
    public class FormBottomHandler : MonoBehaviourPun
    {
        [Header("Okna formularza")]
        [SerializeField] private GameObject allForm;
        [SerializeField] private GameObject background;
        [SerializeField] private GameObject resultWindowPass;
        [SerializeField] private GameObject resultWindowFailed;

        [Header("Wyniki formularza")]
        [SerializeField] private Text nameFromField;
        [SerializeField] private Toggle intField;
        [SerializeField] private Toggle stringField;
        [SerializeField] private Toggle floatField;
        [SerializeField] private Text dropDownMenu;

        [Header("Zmienne do okna rezultatu")]
        [SerializeField] private GameObject door = null;

        private bool check;
        private string nameInput;
        private string option;
        public static int successCounter = 0;
        byte COMPLETE_FORM_EVENT = 1;


        // Update is called once per frame
        void Update()
        {
            if (intField.GetComponent<Toggle>().isOn == true && stringField.GetComponent<Toggle>().isOn == false && floatField.GetComponent<Toggle>().isOn == true)
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

            if (check == true && nameInput != "" && option == "Java")
            {

                CompleteForm();
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
            if (successCounter >= PhotonNetwork.CurrentRoom.PlayerCount)
            {
                Debug.Log($"Number of Players: {PhotonNetwork.CurrentRoom.PlayerCount}");
                Debug.Log($"Success Counter: {successCounter}");
                door.gameObject.GetComponent<DoorScript>().OpenTheDoor();
            }
            resultWindowPass.SetActive(false);
            background.SetActive(false);
            allForm.SetActive(false);
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
                Debug.Log("Received Event");
            }
        }

        private void CompleteForm()
        {
            Debug.Log("Form Completed");
            Photon.Realtime.RaiseEventOptions raiseEventOptions = new Photon.Realtime.RaiseEventOptions { Receivers = Photon.Realtime.ReceiverGroup.Others };
            ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { };
            PhotonNetwork.RaiseEvent(COMPLETE_FORM_EVENT, new object[] { }, raiseEventOptions, sendOptions);
            AddToSuccessCounter();
        }

        public void AddToSuccessCounter()
        {
            successCounter += 1;
            Debug.Log($"Add Success! Current value: {successCounter}");
        }
    }
}