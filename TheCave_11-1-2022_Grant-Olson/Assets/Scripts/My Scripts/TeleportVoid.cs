using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class TeleportVoid : MonoBehaviour
{
    public GameObject shadow;
    private SpriteRenderer shadowSprite;
    private Animator shadowAnim;
    private RuntimeAnimatorController shadowController;
    public GameObject player;
    private SpriteRenderer playerSprite;
    private Animator playerAnim;
    private RuntimeAnimatorController playerController;
    public GameObject exitTelePort;
    private Transform playerGrabber;

    private void Start() 
    {
        shadowSprite = shadow.GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        shadowAnim = shadow.GetComponent<Animator>();
        playerAnim = player.GetComponent<Animator>();
        shadowController = shadowAnim.runtimeAnimatorController;
        playerController = playerAnim.runtimeAnimatorController;

        playerGrabber = exitTelePort.transform; 
    }
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision) 
    {
       if (collision.gameObject.CompareTag("Player"))
       {
            player.transform.position = playerGrabber.position;
            shadowAnim.runtimeAnimatorController = playerController;
            playerAnim.runtimeAnimatorController = shadowController;

       } 
    }
    
}
