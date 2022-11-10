using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public PlatformManager platformManagerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreCounter.coinAmount += 1;
        platformManagerScript.NewPlatform();
    }
}
