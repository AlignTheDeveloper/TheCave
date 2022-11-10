using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectibleDestruction : MonoBehaviour
{
    private SpriteRenderer sprite;
    private CircleCollider2D coll;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sprite.enabled = false;
            coll.enabled = false;

            StartCoroutine("RespawnDiamond");
        }
    }
    IEnumerator RespawnDiamond()
    {
        yield return new WaitForSeconds(3);
        sprite.enabled = true;
        coll.enabled = true;
    }


}

