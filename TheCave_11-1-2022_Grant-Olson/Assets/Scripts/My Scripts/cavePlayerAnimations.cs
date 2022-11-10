using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cavePlayerAnimations : MonoBehaviour
// Simple script which uses enum to trigger idle, run, jump, and fall animations.
{
    // create containers (variables) for specific unity objects.
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX = 0f;
    private enum MoveState {idle, run, jump, fall}
    // variable created to store the animation state so it can be sent to shadow.
    public int currentState;


    // Start is called before the first frame update and is often used to assign objects to containers
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        UpdateAnimationState();
    }


    // Method used to make animations line up with the character's movement.
    public void UpdateAnimationState()
    {
        MoveState state;

        if (dirX > 0f)
        {
            state = MoveState.run;
            sprite.flipX = false;
            
        }
        else if (dirX < 0f)
        {
            state = MoveState.run;
            sprite.flipX = true;
            
        }
        else
        {
            state = MoveState.idle;
            
        }

        if (rb.velocity.y > 0.01f)
        {
            state = MoveState.jump;
            
        }
        else if (rb.velocity.y < -0.01f)
        {
            state = MoveState.fall;
            
        }

        anim.SetInteger("State", (int)state);
        // set currentState to an integer of it's enum value in order to send it to shadow
        currentState = (int)state;
    }

}
