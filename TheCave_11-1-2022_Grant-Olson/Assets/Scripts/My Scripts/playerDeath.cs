using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
// Script which makes the player unable to move, plays a death animation, and
// resets the player at the last checkpoint the last checkpoint they touched
// resets the player if they have light and die
{
    public GameObject cpParent;
    private Transform cpGrabber;
    private Vector3 spawnPoint;
    public GameObject player;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private int deathCount;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        deathCount = 0;

        //grabs the position of the starting area
        cpGrabber = cpParent.transform.GetChild(0);
        player.transform.position = cpGrabber.position;
        
    }

   private void OnCollisionEnter2D(Collision2D collision) 
   {
        if (collision.gameObject.CompareTag("Traps"))
        {
            Die();
        }
   }

    // turn the player's movement off and play the death animation
    // add one death to the death counter
   private void Die()
   {
    rb.bodyType = RigidbodyType2D.Static;
    anim.SetTrigger("Death");
    deathCount ++;
   }

    // when colliding with a checkpoint, save the checkpoints tranform in cpGrabber
   private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            cpGrabber = other.transform;
        }
    }

    //Reset Player and move them to the last checkpoint they touched
    private void Respawn()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.ResetTrigger("Death");
        player.transform.position = cpGrabber.position;
        anim.Play("Shadow_Idle");
              
    }
}
