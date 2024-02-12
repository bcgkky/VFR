using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forSofa : MonoBehaviour
{

    [SerializeField]
    GameObject pressEPanel0, pressEPanel1, player, camForSofa0, _forStand;
    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pressEPanel0.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                Invoke("_forStandSofa" ,7f);
                pressEPanel0.SetActive(false);
                player.SetActive(false);
                camForSofa0.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pressEPanel0.SetActive(false);
        }
    }
    void _forStandSofa()
    {
        pressEPanel1.SetActive(true);
        _forStand.SetActive(true);
    }
}
