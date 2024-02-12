using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class g2_bomb : MonoBehaviour
{

    GameObject cam;
    Rigidbody rb;
    AudioSource bomb_au;
    public static Vector3 bomb_pos;
    GameObject for_BombPos;
    void Start()
    {
        for_BombPos = GameObject.FindGameObjectWithTag("bomb_ef");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        rb = GetComponent<Rigidbody>();
        rb.AddForce(cam.transform.forward * 500f);
        bomb_au = GameObject.FindGameObjectWithTag("bomb_au").GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground") || other.gameObject.CompareTag("enemy_boss") || other.gameObject.CompareTag("enemy_mid") || other.gameObject.CompareTag("enemy_low") || other.gameObject.CompareTag("enemy_skill") || other.gameObject.CompareTag("fast_skill_enemy") || other.gameObject.CompareTag("range_skill_enemy") || other.gameObject.CompareTag("upgrade_skill_enemy"))
        {
            gameObject.GetComponent<SphereCollider>().radius = 1;
            for_BombPos.GetComponent<g2_bomb_ac>().enabled = true;
            for_BombPos.transform.position = transform.position;
            for_BombPos.GetComponentInChildren<ParticleSystem>().Play();
            Destroy(transform.gameObject,.1f);
        }
    }
 }
