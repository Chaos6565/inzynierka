using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{

    [SerializeField]
    private ConnectionSettings _connectionSettings;

    public static ConnectionSettings ConnectionSettings { get { return Instance._connectionSettings; } }

}
