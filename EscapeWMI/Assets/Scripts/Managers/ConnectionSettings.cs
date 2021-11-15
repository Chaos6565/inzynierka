using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/ConnectionSettings")]
public class ConnectionSettings : ScriptableObject
{
    [SerializeField]
    private string _gameVersion = "0.0.0";

    public string GameVersion { get { return _gameVersion; } }

    [SerializeField]
    private string _nickName = "DefaultName";

    private const string PlayerPrefsNameKey = "PlayerName";

    [SerializeField]
    private int _minPlayersPerRoom = 3;

    public int MinPlayersPerRoom { get { return _minPlayersPerRoom; } }


    public string NickName
    {
        get
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { _nickName = "Player" + Random.Range(100, 999).ToString(); }
            else { _nickName = PlayerPrefs.GetString(PlayerPrefsNameKey); }
            return _nickName;
        }
    }
}
