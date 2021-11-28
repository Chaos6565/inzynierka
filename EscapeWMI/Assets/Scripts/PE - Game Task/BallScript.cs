using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float power = 10f;
    public Rigidbody2D rb;
    public Camera camera;

    public Vector2 minPower;
    public Vector2 maxPower;

    public Vector2 force;
    public Vector3 startPos;
    public Vector3 endPos;

    TrajectoryLine tl;

    public bool end = true;

    // Start is called before the first frame update
    void Start()
    {
        tl = GetComponent<TrajectoryLine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (end == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = camera.ScreenToWorldPoint(Input.mousePosition);
                startPos.z = 15;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 currentPos = camera.ScreenToWorldPoint(Input.mousePosition);
                currentPos.z = 15;

                tl.RenderLine(startPos, currentPos);
            }

            if (Input.GetMouseButtonUp(0))
            {
                endPos = camera.ScreenToWorldPoint(Input.mousePosition);
                endPos.z = 15;

                force = new Vector2(Mathf.Clamp(startPos.x - endPos.x, minPower.x, maxPower.x), Mathf.Clamp(startPos.y - endPos.y, minPower.y, maxPower.y));

                rb.AddForce(force * power, ForceMode2D.Impulse);

                tl.EndLine();

                end = false;
            }
        }
    }
}
