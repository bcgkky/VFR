using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sofaStand : MonoBehaviour
{

    [SerializeField]
    GameObject player, sofaStandCam, pressEPanel1;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pressEPanel1.SetActive(false);
            sofaStandCam.GetComponent<Animator>().SetBool("standSofa",true);
            Invoke("_forStand",2.44f);
        }
    }

    void _forStand()
    {
        sofaStandCam.GetComponent<Animator>().SetBool("standSofa", false);
        player.SetActive(true);
        sofaStandCam.SetActive(false);
        gameObject.SetActive(false);
    }
}
