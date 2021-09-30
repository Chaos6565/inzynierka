using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerScript : MonoBehaviour
{
    public Text pointText;

    public float speed;

    Camera camera;
    Vector2 topLeft;
    Vector2 topRight;

    bool test = true;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        topLeft = (Vector2)camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, camera.nearClipPlane));
        topRight = (Vector2)camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, camera.nearClipPlane));
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(topLeft.x + 3))
            test = false;
        if (Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(topRight.x - 3))
            test = true;

        if(test == true)
            transform.position = Vector3.Lerp(transform.position, new Vector3(topLeft.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
        if(test == false)
            transform.position = Vector3.Lerp(transform.position, new Vector3(topRight.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        pointText.text = "1";
    }

}

