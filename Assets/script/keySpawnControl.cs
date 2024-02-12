using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class keySpawnControl : MonoBehaviour
{

    public GameObject Key, keyWay;
    public GameObject[] keySpawns;
    public bool keyAlive = true;

    public float _forTime;
    float __fortTime;
    public float keyWaySec;
    public Text _timerText;


    void Start()
    {
        Invoke("_forKeyWayFalse", keyWaySec);

        __fortTime = _forTime;
        StartCoroutine(_forTimer());
        int rn = Random.Range(0, 5);
        Key.SetActive(true);
        keyWay.SetActive(true);
        Key.transform.position = keySpawns[rn].transform.position;
        keyWay.transform.position = new Vector3(keySpawns[rn].transform.position.x,83f, keySpawns[rn].transform.position.z);
    }

    public IEnumerator kewSpawns()
    {
        while (keyAlive)
        {
            print("keySpawnTime11 =" + _forTime);
            yield return new WaitForSeconds(_forTime);
            print("keySpawnTime22 =" + _forTime);
            int rnd = Random.Range(0, 5);
            Key.transform.position = keySpawns[rnd].transform.position;
            keyWay.SetActive(true);
            keyWay.transform.position = new Vector3(keySpawns[rnd].transform.position.x,83f, keySpawns[rnd].transform.position.z);
            yield return new WaitForSeconds(keyWaySec);
            keyWay.SetActive(false);
        }
        
    }

    IEnumerator _forTimer()
    {
        while (_forTime > -1)
        {
            yield return new WaitForSeconds(1f);
            _forTime--;

            if (_forTime == 2 && keyAlive)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }

            if (_forTime == -1)
            {
                _forTime = __fortTime;
            }
        }
    }

    void Update()
    {
        if (keyAlive)
        {
            _timerText.text = _forTime.ToString();
        }

        if (!keyAlive)
        {
            StopCoroutine(kewSpawns());
            StopCoroutine(_forTimer());
            _timerText.text = "-";
        }
    }

    void _forKeyWayFalse()
    {
        keyWay.SetActive(false);
        StartCoroutine(kewSpawns());
    }
}
