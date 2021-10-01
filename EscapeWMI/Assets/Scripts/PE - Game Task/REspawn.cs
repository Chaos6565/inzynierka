using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REspawn : MonoBehaviour
{

    private PEScript PE;
    private TriggerScript TS;
    public GameObject Capsule;
    public GameObject Ball;
    GlobalTime GT;
    
    

    // Start is called before the first frame update
    void Start()
    {
        PE = Ball.GetComponent<PEScript>();
        TS = Capsule.GetComponent<TriggerScript>();
        GT = GetComponent<GlobalTime>();
    }

    // Update is called once per frame
    void Update()
    {
        respawn();
    }

    public void respawn()
    {
        if (GT.seconds <= 0 && TS.win == false)
        {
            GT.seconds = 11;
            PE.end = true;
            PE.transform.position = new Vector3(0.37f, -7.23f, 0f);
            PE.rb.velocity = new Vector2(0f, 0f);
        }
    }
}
