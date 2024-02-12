using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class forOpenDoor : MonoBehaviour
{

    public GameObject door, unlockerKey, finishWall;
    public bool unLock = false;
    bool open = true;
    bool thatPressF = false;
    public GameObject pressF;

    public float npcStoptime;


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !thatPressF && !GameObject.FindGameObjectWithTag("keySpawner").GetComponent<keySpawnControl>().keyAlive)
        {
            pressF.SetActive(true);
            if (unLock && open && Input.GetKey(KeyCode.F))
            {
                pressF.SetActive(false);
                thatPressF = true;
                unlockerKey.SetActive(true);
                unlockerKey.GetComponent<Animator>().Play("unlockerKEy");
                finishWall.SetActive(true);
                StartCoroutine(_forOpenDoor());
                open = false;
                GameObject.FindGameObjectWithTag("Lavuk").GetComponent<NavMeshAgent>().enabled = false;
                Invoke("_forNPCstopOnOpenDoor",npcStoptime);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pressF.SetActive(false);
        }
    }

    IEnumerator _forOpenDoor()
    {
        yield return new WaitForSeconds(2.32f);
        unlockerKey.GetComponent<Animator>().Play("unlockerKEy2");
        yield return new WaitForSeconds(1.1f);
        unlockerKey.GetComponent<Animator>().Play("unlockerKEy1");
        door.GetComponent<Animator>().Play("forDoorAnim");
        yield return new WaitForSeconds(2f);
        finishWall.GetComponent<BoxCollider>().enabled = true;


    }

    void _forNPCstopOnOpenDoor()
    {
        GameObject.FindGameObjectWithTag("Lavuk").GetComponent<NavMeshAgent>().enabled = true;

    }
}
