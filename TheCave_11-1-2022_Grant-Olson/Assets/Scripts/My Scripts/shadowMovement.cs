using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowMovement : MonoBehaviour
// Script that is used to copy the animations of the player gameObject in order to sync the shadow and player. 
//Note: Could easily be made to desync the two with delta time for cool effect.
{
    // creating containers that will be used in script
    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 0f;
    // creates a container which can reference an outside script
    private cavePlayerAnimations player;
    private enum ShadowState {idle, run, jump, fall}


    // Start is called before the first frame update and is used to assign values to containers
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        // assign the script reference to the script reference variable
        player = FindObjectOfType<cavePlayerAnimations>();
        
    }


    // Every frame, check these things
    private void Update() 
    {
        dirX = Input.GetAxisRaw("Horizontal");
    }
   
    
    // Every frame, right after update, check these things
    void LateUpdate()
    { 
        UpdateAnimationState();
    }

    // method that copies the player's animations using a value sent from the cavePlayerAnimations script
    private void UpdateAnimationState()
    {
        
        if (dirX > 0f)
        {
            sprite.flipX = false;
        }
        if (dirX < 0f)
        {
            sprite.flipX = true;
        }
        anim.SetInteger("State", (int)player.currentState);
    }
}
