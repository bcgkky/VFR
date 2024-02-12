using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class forPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject diePanel, finishPanel, pausePanel, vfr, CamVFR, pressVFR, pressPiano,
        diffPanel, piano, doorExitPanel, m1, m2, blockWall, pressEblock, loadScenePanel, game_Select_Panel;
    
    

    [SerializeField]
    public AudioSource auKEY;

    bool pause = false;
    bool thatFinish = false;
    bool pianoPlay = false;
    bool thatPause = false;
    bool muteInPause = false;
    bool blocked = false;
    bool canSelect = false;

    public bool thatVFRbox = false;

    float forPianoPressTime = 0;
    float blockWaitPress = 0;

    private void Start()
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PlayerPrefs.GetFloat("vol") == 1)
            {
                m1.SetActive(true);
            }
            else m2.SetActive(true);


            pause = !pause;
            if (pause)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
                thatPause = true;
            }
            else
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
                thatPause = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.M) && thatPause && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0); 
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.K) && thatPause)
        {
            muteInPause= !muteInPause;
            if (muteInPause && PlayerPrefs.GetFloat("vol") == 1)
            {
                m1.SetActive(false);
                m2.SetActive(true);
                PlayerPrefs.SetFloat("vol", 0f);
                AudioListener.volume = PlayerPrefs.GetFloat("vol");

            }
            else
            {
                m1.SetActive(true);
                m2.SetActive(false);
                PlayerPrefs.SetFloat("vol", 1f);
                AudioListener.volume = PlayerPrefs.GetFloat("vol");
            }
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("vfr") || other.gameObject.CompareTag("piano") || other.gameObject.CompareTag("youCanTpassHere"))
        {
           thatVFRbox= false;
            pressVFR.SetActive(false);
            pressPiano.SetActive(false);
            doorExitPanel.SetActive(false);
        }
        if (other.gameObject.CompareTag("block"))
        {
            pressEblock.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("block"))
        {
            pressEblock.SetActive(true);
            if (Input.GetKey(KeyCode.E) && Time.time > blockWaitPress )
            {
                blockWaitPress = Time.time + 1f;
                blocked = !blocked;
                if (blocked)
                {
                    blockWall.GetComponent<Animator>().Play("blockWall");
                }else blockWall.GetComponent<Animator>().Play("blockWall1");
            }
        }

        if (other.gameObject.CompareTag("youCanTpassHere"))
        {
            doorExitPanel.SetActive(true);
        }

        if (other.gameObject.CompareTag("vfr"))
        {
            thatVFRbox = true;
            pressVFR.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                pressVFR.SetActive(false);
                CamVFR.SetActive(true);
                gameObject.SetActive(false);
                vfr.SetActive(false);
                Invoke("_forVFRanim", 2.8f);
                Invoke("_forVFRJoinGame", 5.7f);
            }
        }
        if (other.gameObject.CompareTag("piano"))
        {
            pressPiano.SetActive(true);
            if (Input.GetKey(KeyCode.E) && Time.time > forPianoPressTime)
            {
                forPianoPressTime = Time.time + .4f;
                pianoPlay = !pianoPlay;
                if (pianoPlay)
                {
                    piano.GetComponent<AudioSource>().enabled = true;
                    GameObject.FindGameObjectWithTag("homeCanvas").GetComponent<AudioSource>().enabled = false;

                }
                else
                {
                    piano.GetComponent<AudioSource>().enabled = false;
                    GameObject.FindGameObjectWithTag("homeCanvas").GetComponent<AudioSource>().enabled = true;
                }
            }
        }
        if (other.gameObject.CompareTag("key"))
        {
            auKEY.Play();
            GameObject.FindGameObjectWithTag("DoorControl").GetComponent<forOpenDoor>().unLock = true;
            Destroy(other.gameObject);
            GameObject.FindGameObjectWithTag("keySpawner").GetComponent<keySpawnControl>().keyAlive = false;
        }
        if (other.gameObject.CompareTag("Lavuk") && !thatFinish)
        {
            GameObject.FindGameObjectWithTag("gameMusic").GetComponent<AudioSource>().enabled= false;
            Invoke("_forSceneControl", 3.8f);
            diePanel.SetActive(true);
            diePanel.GetComponent<Animator>().Play("diePanelAnim");
            GameObject.FindGameObjectWithTag("Lavuk").GetComponent<forNPC>()._isAlive = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
            Invoke("_forTimeScale", 4f);
        }
        if (other.gameObject.CompareTag("youOk"))
        {
            GameObject.FindGameObjectWithTag("gameMusic").GetComponent<AudioSource>().enabled = false;
            thatFinish = true;
            Invoke("_forSceneControl", 3.8f);
            finishPanel.SetActive(true);
            finishPanel.GetComponent<Animator>().Play("finishPanelAnim");
            GameObject.FindGameObjectWithTag("Lavuk").GetComponent<forNPC>()._isAlive = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
            Invoke("_forTimeScale", 4f);
        }
    }
    void _forTimeScale()
    {
        Time.timeScale = 0;
    }
    void _forSceneControl()
    {
        GameObject.FindGameObjectWithTag("forScene").GetComponent<gameSceneControl>().enabled = true;
    }
    void _forVFRanim()
    {
        vfr.SetActive(true);
        vfr.GetComponent<Animator>().Play("vfrAnim");
        
    }
    void _forVFRJoinGame()
    {
        game_Select_Panel.SetActive(true);
    }  
}

