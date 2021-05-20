using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animation;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRen;

    float runSpeed = 7;


    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, Input.GetAxisRaw("Vertical") * runSpeed);

        animation.SetFloat("MoveX", rb2d.velocity.x);
        animation.SetFloat("MoveY", rb2d.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1
            || Input.GetAxisRaw("Vertical") == -1)
        {
            animation.SetFloat("lastX", Input.GetAxisRaw("Horizontal"));
            animation.SetFloat("lastY", Input.GetAxisRaw("Vertical"));
        }

    }
}
