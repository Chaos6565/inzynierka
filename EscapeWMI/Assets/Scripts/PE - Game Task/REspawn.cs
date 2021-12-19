using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REspawn : MonoBehaviour
{
    private BallScript BS;
    private GoalScript TS;
    public GameObject Capsule;
    public GameObject Ball;
    GlobalTime GT;

    PEManager PM;
    public GameObject PMMan;

    public Camera camera;
    public RectTransform rectTransform_canvas;
    public RectTransform rectTransform_ball;
    
    
    // Start is called before the first frame update
    void Start()
    {
        BS = Ball.GetComponent<BallScript>();
        TS = Capsule.GetComponent<GoalScript>();
        GT = GetComponent<GlobalTime>();

        PM = PMMan.GetComponent<PEManager>();
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
            BS.end = true;

          

            BS.rb.velocity = new Vector2(0f, 0f);

            PM.Result(false);
        }
    }
}
