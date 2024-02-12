using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameSceneControl : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(1); //oda
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //game
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0); // main
            Time.timeScale = 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>()._forCursor();
        }
    }
    
}

