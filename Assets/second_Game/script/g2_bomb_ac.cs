using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g2_bomb_ac : MonoBehaviour
{
    private void OnEnable()
    {
       
        print("hi");
        Invoke("_forBombAct",.8f);
        Invoke("_forDisable",.9f);

    }



    void _forDisable()
    {
        this.GetComponent<g2_bomb_ac>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }
    

    void _forBombAct()
    {
        gameObject.GetComponent<SphereCollider>().enabled = true;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
