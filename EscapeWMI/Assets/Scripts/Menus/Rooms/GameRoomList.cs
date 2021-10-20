using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomList : MonoBehaviour
{
    [SerializeField]
    private CreateRoomMenu _createRoomMenu;
    [SerializeField]
    private RoomListingsMenu _roomListingsMenu;

    private MainMenuScript _mainMenuScript;

    public void FirstInitialize(MainMenuScript mainMenuScriptReference)
    {
        _mainMenuScript = mainMenuScriptReference;
        _createRoomMenu.FirstInitialize(mainMenuScriptReference);
        _roomListingsMenu.FirstInitialize(mainMenuScriptReference);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
