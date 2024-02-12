using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class forDiff : MonoBehaviour
{

    public GameObject forDiffNpcOpenDoorTime;
    public GameObject npc_STOP;

    private void Start()
    {
        npc_STOP.GetComponent<NavMeshAgent>().enabled = false;
        Time.timeScale = 0f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
        GameObject.FindGameObjectWithTag("gameMusic").GetComponent<AudioSource>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _gameStandart();

            GameObject.FindGameObjectWithTag("Lavuk").GetComponent<NavMeshAgent>().speed= 4f;
            GameObject.FindGameObjectWithTag("keySpawner").GetComponent<keySpawnControl>()._forTime = 34;
            GameObject.FindGameObjectWithTag("keySpawner").GetComponent<keySpawnControl>().keyWaySec= 8;
            GameObject.FindGameObjectWithTag("DoorControl").GetComponent<forOpenDoor>().npcStoptime= 5;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _gameStandart();

            GameObject.FindGameObjectWithTag("Lavuk").GetComponent<NavMeshAgent>().speed = 7f;
            GameObject.FindGameObjectWithTag("keySpawner").GetComponent<keySpawnControl>()._forTime = 24;
            GameObject.FindGameObjectWithTag("keySpawner").GetComponent<keySpawnControl>().keyWaySec = 4;
            GameObject.FindGameObjectWithTag("DoorControl").GetComponent<forOpenDoor>().npcStoptime = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _gameStandart();
            GameObject.FindGameObjectWithTag("Lavuk").GetComponent<NavMeshAgent>().speed = 8.4f;
            GameObject.FindGameObjectWithTag("keySpawner").GetComponent<keySpawnControl>()._forTime = 13;
            GameObject.FindGameObjectWithTag("keySpawner").GetComponent<keySpawnControl>().keyWaySec = 1;
            GameObject.FindGameObjectWithTag("DoorControl").GetComponent<forOpenDoor>().npcStoptime = .8f;


        }
    }
    void _gameStandart()
    {
        Destroy(gameObject);
        Time.timeScale = 1.0f;
        GameObject.FindGameObjectWithTag("Lavuk").GetComponent<AudioSource>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
        GameObject.FindGameObjectWithTag("gameMusic").GetComponent<AudioSource>().enabled = true;
        GameObject.FindGameObjectWithTag("keySpawner").GetComponent<keySpawnControl>().enabled = true;
        npc_STOP.GetComponent<NavMeshAgent>().enabled = true;
        forDiffNpcOpenDoorTime.SetActive(true);
    }
}
