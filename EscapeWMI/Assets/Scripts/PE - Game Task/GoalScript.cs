using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScript : MonoBehaviour
{
    public Text pointText;
    public bool win = false;

    public float speed;

    PEManager PM;
    public GameObject PMMan;

    public Camera camera;
    Vector2 topLeft;
    Vector2 topRight;

    bool test = true;

    // Start is called before the first frame update
    void Start()
    {
        topLeft = (Vector2)camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, camera.nearClipPlane));
        topRight = (Vector2)camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, camera.nearClipPlane));

        PM = PMMan.GetComponent<PEManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(topLeft.x + 30))
            test = false;
        if (Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(topRight.x - 30))
            test = true;

        if(test == true)
            transform.position = Vector3.Lerp(transform.position, new Vector3(topLeft.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
        if(test == false)
            transform.position = Vector3.Lerp(transform.position, new Vector3(topRight.x, transform.position.y, transform.position.z), Time.deltaTime * speed);


        if (win == true)
        {
            PM.Result(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        pointText.text = "1";
        win = true;
    }

   

}

