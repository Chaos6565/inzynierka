using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WMI.Menus
{
    public class PlayerLogIn : MonoBehaviour
    {
        [SerializeField]
        private InputField nameInputField = null;
        [SerializeField]
        private Button loginButton = null;

        private const string PlayerPrefsNameKey = "PlayerName";

        private void Start() => SetUpInputField();

        private void SetUpInputField()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }

            string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

            Debug.Log("Default name is: " + defaultName);

            nameInputField.text = defaultName;

            SetPlayerName(defaultName);
        }

        public void SetPlayerName(string name)
        {
            loginButton.interactable = !string.IsNullOrEmpty(name);
        }

        public void SavePlayerName()
        {
            string playerName = nameInputField.text;

            PhotonNetwork.NickName = playerName;

            PlayerPrefs.SetString(PlayerPrefsNameKey, playerName);
        }

    }

}
