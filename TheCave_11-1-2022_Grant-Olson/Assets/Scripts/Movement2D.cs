using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public bool isGrounded;
    private SpriteRenderer sprite;

    public void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        if (Input.GetAxis("Horizontal") > 0)
        {
            sprite.flipX = true;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            sprite.flipX = false;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(0f, jumpForce));
        }    
    }
}

