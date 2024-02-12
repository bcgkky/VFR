using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g2_IconSkill : MonoBehaviour
{

    float dist =0f;

    void Update()
    {

        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
        dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (dist < 10)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, 10f * Time.deltaTime);
        }

    }
}
