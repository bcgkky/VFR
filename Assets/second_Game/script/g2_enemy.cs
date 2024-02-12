using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class g2_enemy : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent agent;
    public int where;

    [SerializeField]
    GameObject[] target; 
    int target_I;
    public static bool canGO=false;
    public static float boss_dmg, mid_dmg, low_dmg, skll_dmg;


    public float boss_health, low_healt, mid_health, skll_health;
    public ParticleSystem die_Part;

    public GameObject mid_part, strong_skill_Icon, fast_skill_Icon, range_skill_Icon, upgrade_skill_Icon, boss_part, low_part;

    AudioSource dead_au;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 0f), Random.Range(-5f, 5f));
        target_I = Random.Range(0, g2_towers.tower_Count);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bomb_ef"))
        {
            print("BOMBBBBBBB");
            if (this.CompareTag("enemy_skill"))
            {
                g2_player.EnemyKill(2, transform.gameObject, 0f, mid_part, strong_skill_Icon, dead_au);
                
            }
            if (this.CompareTag("fast_skill_enemy"))
            {
                g2_player.EnemyKill(2, transform.gameObject, 0f, mid_part, fast_skill_Icon, dead_au);
            }
            if (this.CompareTag("range_skill_enemy"))
            {
                g2_player.EnemyKill(2, transform.gameObject, 0f, mid_part, range_skill_Icon, dead_au);
            }
            if (this.CompareTag("upgrade_skill_enemy"))
            {
                g2_player.EnemyKill(2, transform.gameObject, 0f, mid_part, upgrade_skill_Icon, dead_au);
            }
            if (this.CompareTag("enemy_boss"))
            {
                g2_player.EnemyKill(1, transform.gameObject, 0f, boss_part, null, dead_au);
            }
            if (this.CompareTag("enemy_mid"))
            {
                g2_player.EnemyKill(1, transform.gameObject, 0f, mid_part, null, dead_au);
            }
            if (this.CompareTag("enemy_low"))
            {
                g2_player.EnemyKill(1, transform.gameObject, 0f, low_part, null, dead_au);
            }
        }
    }

    void Start()
    {
        dead_au = GameObject.FindGameObjectWithTag("enemy_die_au").GetComponent<AudioSource>();

        canGO = false;
        target = GameObject.FindGameObjectsWithTag("tower");

        agent = GetComponent<NavMeshAgent>();
        if (this.CompareTag("enemy_boss"))
        {
            boss_health = 10;
            boss_dmg = 2f;
        }
        if (this.CompareTag("enemy_mid"))
        {
            mid_health = 6;
            mid_dmg = 1.4f;
        }
        if (this.CompareTag("enemy_low"))
        {
            low_healt = 3;
            low_dmg = .7f;
        }
        if (this.CompareTag("enemy_skill")|| this.CompareTag("fast_skill_enemy")|| this.CompareTag("range_skill_enemy")|| this.CompareTag("upgrade_skill_enemy"))
        {
            skll_health = 12;
            skll_dmg = .3f;
        }
    }
 
    void Update()
    {

        if (canGO)
        {
            
            if (target[target_I].transform.gameObject.GetComponent<g2_towers>().tower_alive)
            {
                where = target_I;
                agent.SetDestination(target[target_I].transform.position);
            }
            if (!target[target_I].transform.gameObject.GetComponent<g2_towers>().tower_alive)
            {

                target = GameObject.FindGameObjectsWithTag("tower");
                agent.SetDestination(target[Random.Range(0, target.Length)].transform.position);

            }
            else return;

            
        }

        if (g2_towers.tower_Count == 0)
        {
            transform.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
    }
    
}
