using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animation2d;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRen;

    public static PlayerController instant;

    float runSpeed = 7;


    // Start is called before the first frame update
    void Start()
    {
        instant = this;
        animation2d = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 move_velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rigidbody2d.velocity = move_velocity * runSpeed;

        animation2d.SetFloat("MoveX", rigidbody2d.velocity.x);
        animation2d.SetFloat("MoveY", rigidbody2d.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1
            || Input.GetAxisRaw("Vertical") == -1)
        {
            animation2d.SetFloat("lastX", Input.GetAxisRaw("Horizontal"));
            animation2d.SetFloat("lastY", Input.GetAxisRaw("Vertical"));
        }

    }
}