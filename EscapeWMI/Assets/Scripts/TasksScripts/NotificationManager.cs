using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public GameObject Canvas;

    public void Exit()
    {
        Canvas.SetActive(false);
    }
    
}

