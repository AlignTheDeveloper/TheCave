using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShadowVisualization : MonoBehaviour
// Script which turns off the shadow and causes it to reappear if the player touches a diamond.
// Then the shadow is always front and center of camera.
{
    private SpriteRenderer sprite;
    private Animator anim;
    public bool hasLight;
    [SerializeField] private CinemachineBrain brain;
    private int lowPriority;
    private int highPriority;
    [SerializeField] private CinemachineVirtualCamera cam1;
    [SerializeField] private CinemachineVirtualCamera cam2;
    

    
    // Start is called before the first frame update. Using it to grab parts of shadow and define priority for use in methods.
    void Start()
    {
        
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        hasLight = false;
        sprite.enabled = false;
        anim.enabled = false;
        lowPriority = 1;
        highPriority = lowPriority + 1;
        cam1.Priority  = highPriority;
        cam2.Priority = lowPriority;

    }

    // Update is called once per frame
    void Update()
    {
        LightCheck();
    }
    
    // Method that is used to check if the player has collected "the light at the end of the tunnel"
    public void LightCheck()
    {
        if (hasLight == true)
        {
            sprite.enabled = true;
            anim.enabled = true;
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Void"))
        {
            cam2.Priority = lowPriority;
            cam1.Priority = highPriority;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Diamond") && hasLight != true)
        {
            sprite.enabled = true;
            anim.enabled = true;
            cam1.Priority = lowPriority;
            cam2.Priority = highPriority;
            
            StartCoroutine("JumpScare");
        }
        if (collision.gameObject.CompareTag("Light"))
        {
            hasLight = true;
            cam1.Priority = lowPriority;
            cam2.Priority = highPriority;
        }
    }

    IEnumerator JumpScare()
    {
        yield return new WaitForSeconds(1);
        sprite.enabled = false;
        anim.enabled = false;
        cam2.Priority = lowPriority;
        cam1.Priority = highPriority;
    }
}
