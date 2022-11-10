using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cavePlayerMovement : MonoBehaviour
// Script that handles all player movement. Yes, all of it. F
{
    // create containers and variables
    [Header("Components")]
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask whatIsGround;


    [Header("Movement Variables")]
    private Vector2 moveInput;
    [SerializeField] private float movementAcceleration = 70f;
    [SerializeField] private float maxMoveSpeed = 12f;
    [SerializeField] private float groundLinearDrag = 7f;
    private float dirX;
    //Snappier direction change.
    //true if rigidbody is moving right and the horizontal input is less than 0 (left key is being pressed)
    //or
    //true if rigidbody is moving left and the horizontal input is greater than 0 (right key is being pressed)
    private bool changingDirection => (rb.velocity.x > 0f && dirX < 0f) || (rb.velocity.x < 0f && dirX > 0f);

    
    [Header("Jump Variables")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float airLinearDrag = 2.5f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    // used for double jump
    private int jumps;
    private int resetJumps = 0;
    //true if jump button is pressed and (on the ground or a wall or you have more than 0 jumps left)
    private bool canJump => Input.GetButtonDown("Jump") && (isGrounded || onWallLeft || onWallRight || jumps > 0);

    
    [Header("Collision Variables")]
    [SerializeField] private float extraHeightTest;
    private bool isGrounded;
    private bool onWallLeft;
    private bool onWallRight;

    

    //Start is called after the first frame
    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    //Update runs every frame
    private void Update() 
    {
        dirX = GetInput().x;
        if (canJump) {Jump();}        
    }

    //FixedUpdate runs at a fixed interval independent of frame rate
    //Use for forces
    private void FixedUpdate() 
    {
        CheckCollision();
        H_Movement();

        if (isGrounded) 
        {
            jumps = resetJumps;
            ApplyGroundLinearDrag();
        }
        else if (onWallLeft || onWallRight)
        {
            ApplyAirLinearDrag();
            FallMultiplier();
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
        }
    }

    //Store inputs for later use
    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    //Move player fluidly across Horizontal Axis
    private void H_Movement()
    {
        rb.AddForce(new Vector2(dirX, 0f) * movementAcceleration);

        if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
        }
    }
    //Makes the ground feel less like ice by applying friction when the movement button isn't pressed or the player is changing direction.
    //will work with a stick too due to use of dirX instead of keypress.
    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(dirX) < 0.2f || changingDirection)
        {
            rb.drag = groundLinearDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    //Applies "extra" gravity to make jump feel less floaty
    private void ApplyAirLinearDrag()
    {
        {
            rb.drag = airLinearDrag;
        }
    }

    //Black magic that makes you fall in a cool way idk.
    //I genuinely dont understand this one, I just ripped it from the internet.
    private void FallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier-1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier -1) * Time.deltaTime;
        }
    }

    //Method for jumping.
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumps--;
    }

    //Assign values to grounding bools. true if collision. False if no collision.
    private void CheckCollision()
    {
        isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, 
                                Vector2.down, extraHeightTest, whatIsGround);
        
        onWallLeft = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, 
                                Vector2.left, extraHeightTest, whatIsGround);
        
        onWallRight = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, 
                                Vector2.right, extraHeightTest, whatIsGround);
    }

    //When the player collides with a diamond, the player gets another jump.
    //can compound.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Diamond"))
        {
            jumps ++;
        }
    }
}