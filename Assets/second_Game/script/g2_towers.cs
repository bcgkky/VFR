using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class g2_towers : MonoBehaviour
{
    float dmg = 0;
    float heal = 5;
    bool canDMG = true;
    bool _forCount = true;
    public bool tower_alive = true;
    public static int tower_Count;
    [SerializeField]
    GameObject heal_Bar;
    public ParticleSystem part, tower_hit_part;
    public GameObject a, b;
    public Text tower_text;

    public AudioSource tower_die;


    void Start()
    {
        dmg = 0.2f;
        tower_Count = 4;
    }

    void Update()
    {
        
        heal_Bar.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);

        if (heal < 0)
        {
            heal_Bar.GetComponent<SpriteRenderer>().size = new Vector2(0f, 1f);
            a.GetComponent<Animator>().enabled = false;
            b.GetComponent<Animator>().enabled = false;
        }

        if (_forCount)
        {
            if (heal < 0f)
            {
                
                part.Play();
                tower_alive = false;
                transform.gameObject.tag = "dead_tower";
                Deadfor_Count();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy_boss") || other.gameObject.CompareTag("enemy_mid") || other.gameObject.CompareTag("enemy_low") || other.gameObject.CompareTag("enemy_skill") || other.gameObject.CompareTag("fast_skill_enemy") || other.gameObject.CompareTag("range_skill_enemy") || other.gameObject.CompareTag("upgrade_skill_enemy"))
        {

            if (other.transform.CompareTag("enemy_boss"))
            {
                heal -= g2_enemy.boss_dmg;
            }
            if (other.transform.CompareTag("enemy_mid"))
            {
                heal -= g2_enemy.mid_dmg;
            }
            if (other.transform.CompareTag("enemy_low"))
            {
                heal -= g2_enemy.low_dmg;
            }
            if (other.transform.CompareTag("enemy_skill") || other.transform.CompareTag("fast_skill_enemy") || other.transform.CompareTag("range_skill_enemy") || other.transform.CompareTag("upgrade_skill_enemy"))
            {
                heal -= g2_enemy.skll_dmg;
            }
            if (canDMG)
            {
                heal_Bar.GetComponent<SpriteRenderer>().size = new Vector2(heal, 1f);
            }
            tower_hit_part.Play();
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);

        }
    }
    void Deadfor_Count()
    {
        tower_alive = false; 
        tower_Count--;
        tower_die.Play();
        tower_text.text = "KULE : " + tower_Count + "/4";
        _forCount = false;
       /* if (tower_Count == 0)
        {
            Time.timeScale = 0;
        }*/
    }
}
