using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public GameObject End;
    // Start is called before the first frame update
    void Start()
    {
        End.SetActive(true);
    }

    public void Play()
    {

        End.SetActive(false);
        
    }
    public void Quit()
    {
        Application.Quit();
    }
}
