using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class forNPC : MonoBehaviour
{

    NavMeshAgent ag;
    float dist;
    public GameObject npcClose;
    public bool _isAlive;

    void Start()
    {
        ag = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        ag.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        dist = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);

        if (dist < 5 && _isAlive)
        {
            npcClose.SetActive(true);
        }
        if (dist > 5 || !_isAlive)
        {
            npcClose.SetActive(false);
        }
    }
}
