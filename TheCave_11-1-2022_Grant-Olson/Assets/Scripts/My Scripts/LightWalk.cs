using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWalk : MonoBehaviour
{
    public CompositeCollider2D terrain;
    public CompositeCollider2D platforms;
    private BoxCollider2D playerColl;
    private BoxCollider2D shadowColl;
    private ShadowVisualization grabLight;
    private bool gotLight;
    // Start is called before the first frame update
    void Start()
    {
        playerColl = GetComponentInParent<BoxCollider2D>();
        shadowColl = GetComponent<BoxCollider2D>();
        terrain = GetComponent<CompositeCollider2D>();
        platforms = GetComponent<CompositeCollider2D>();
        shadowColl.enabled = false;
        grabLight = FindObjectOfType<ShadowVisualization>();
        gotLight = grabLight.hasLight;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (gotLight == true)
        {
            playerColl.enabled = false;
            shadowColl.enabled = true;
        }
    }
    
}
