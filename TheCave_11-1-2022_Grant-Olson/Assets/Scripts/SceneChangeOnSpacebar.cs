using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnSpacebar : MonoBehaviour
{
    public int sceneIndex;
    private void Update() 
    {
        LoadOnSpace();
    }

    public void LoadOnSpace()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
