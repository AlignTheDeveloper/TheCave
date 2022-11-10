using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Button button;
    public Image image;
    public TMP_Text text;
    // Start is called before the first frame update
    private void Awake() 
    {
        button.enabled = false;
        image.enabled = false;
        text.enabled = false;
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        StartCoroutine("MenuTimer");
    }

    void Appear()
    {
        sprite.enabled = true;
        button.enabled = true;
        image.enabled = true;
        text.enabled = true;
    }

    IEnumerator MenuTimer()
    {
        yield return new WaitForSeconds(28);
        Appear();
    }
}
