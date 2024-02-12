using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class g2_player : MonoBehaviour
{

    RaycastHit hit;
    public GameObject cam;
    public GameObject gun , skill_Icon , fast_skill_Icon , range_skill_Icon, upgrade_skill_Icon, bomb;
    public ParticleSystem hit_Part;
    float hit_time;
    float hit_time_2 = .25f;
    float range = 25f;
    float bomb_time = 3f;
    int bombs = 5;

    float hit_dmg = 1.5f;

    bool fast_shot = false;
    bool pause = false;
    bool thatPause = false;
    bool muteInPause = false;
    public bool canBomb = false;
    public static bool game_over = false;
    public GameObject pausePanel, m1, m2, dead_Panel,boss_part, mid_part, low_part, stat_Panel, bomb_text;

    public AudioSource hit_enemy, shoot, dead_enemy, upgrade, fall, bomb_throw;
    
  
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("skill"))
        {
            StartCoroutine(statInfo(1));
            hit_dmg += .5f;
            upgrade.Play();
            canBomb = true;
            Destroy(other.gameObject);

        }
        if (other.CompareTag("fast_skill"))
        {
            StartCoroutine(statInfo(2));
            fast_shot = true;
            upgrade.Play();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("range_skill"))
        {
            StartCoroutine(statInfo(3));
            bombs++;
            range = 35f;
            upgrade.Play();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("upgrade_skill"))
        {
            StartCoroutine(statInfo(4));
            bombs++;
            range += 15f;
            hit_dmg += .5f;
            hit_time_2 = .15f;
            upgrade.Play();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("space_Die"))
        {
            GameOver();
        }
        if (other.CompareTag("falled"))
        {
            fall.Play();
        }
    }

    void Update()
    {
        if (canBomb)
        {
            bomb_text.SetActive(true);
            bomb_text.GetComponentInChildren<Text>().text = "BOMBA : " + bombs;
            if (Input.GetKeyDown(KeyCode.G) && Time.time > bomb_time && bombs != 0)
            {
                bomb_time = Time.time + 4f;
                Instantiate(bomb, cam.transform.position, Quaternion.identity);
                bomb_throw.Play();
                bombs--;
                
            }
        }



        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PlayerPrefs.GetFloat("vol") == 1)
            {
                m1.SetActive(true);
            }
            else m2.SetActive(true);


            pause = !pause;
            if (pause)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>().enabled = false;
                thatPause = true;
            }
            else
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>().enabled = true;
                thatPause = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.K) && thatPause)
        {
            muteInPause = !muteInPause;
            if (muteInPause && PlayerPrefs.GetFloat("vol") == 1)
            {
                m1.SetActive(false);
                m2.SetActive(true);
                PlayerPrefs.SetFloat("vol", 0f);
                AudioListener.volume = PlayerPrefs.GetFloat("vol");

            }
            else
            {
                m1.SetActive(true);
                m2.SetActive(false);
                PlayerPrefs.SetFloat("vol", 1f);
                AudioListener.volume = PlayerPrefs.GetFloat("vol");

            }
        }

        Shooting();

        if (g2_towers.tower_Count == 0 && !g2_spawner.game_finish)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        game_over = true;
        dead_Panel.SetActive(true);
        Invoke("_forSceneControl", 3.8f);
        dead_Panel.GetComponent<Animator>().Play("diePanelAnim");
        GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>().enabled = false;
        GameObject.FindGameObjectWithTag("gameMusic").GetComponent<AudioSource>().Stop();
        Invoke("_forTimeScale", 4f);
    }
    void _forSceneControl()
    {
        GameObject.FindGameObjectWithTag("forScene").GetComponent<gameSceneControl>().enabled = true;
    }
    void _forTimeScale()
    {
        Time.timeScale = 0;
    }
    
    IEnumerator statInfo(int stat_Id)
    {
        if (stat_Id == 1)
        {
            stat_Panel.GetComponentInChildren<Text>().text = "+GÜÇ / +5 BOMBA";
        }
        if (stat_Id == 2)
        {
            stat_Panel.GetComponentInChildren<Text>().text = "+HIZLI ATIÞ";
        }
        if (stat_Id == 3)
        {
            stat_Panel.GetComponentInChildren<Text>().text = "+MENZÝL / +1 BOMBA";
        }
        if (stat_Id == 4)
        {
            stat_Panel.GetComponentInChildren<Text>().text = "+YETENEKLER / +1 BOMBA";
        }
        stat_Panel.SetActive(true);
        yield return new WaitForSeconds(2f);
        stat_Panel.SetActive(false);
    }

    void Shooting()
    {

        if (g2_spawner.info_)
        {
            if (fast_shot)
            {
                if (Input.GetMouseButton(0) && Time.time > hit_time)
                {
                    int lyr = 1 << 8;
                    lyr = ~lyr;
                    hit_time = Time.time + hit_time_2;
                    gun.GetComponent<Animator>().Play("gun_mouse1");
                    shoot.Play();
                    if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, lyr))
                    {
                        if (hit.transform.CompareTag("enemy_boss") || hit.transform.CompareTag("enemy_low") || hit.transform.CompareTag("enemy_mid") || hit.transform.CompareTag("enemy_skill") || hit.transform.CompareTag("fast_skill_enemy") || hit.transform.CompareTag("range_skill_enemy") || hit.transform.CompareTag("upgrade_skill_enemy"))
                        {
                            Instantiate(hit_Part, hit.point, Quaternion.identity);
                            hit_enemy.Play();

                            if (hit.transform.CompareTag("enemy_boss"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().boss_health -= hit_dmg;
                                EnemyKill(1, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().boss_health, boss_part, null, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().boss_health <= 0)
                                {
                                    Instantiate(boss_part, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                            if (hit.transform.CompareTag("enemy_mid"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().mid_health -= hit_dmg;
                                EnemyKill(1, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().mid_health, mid_part, null, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().mid_health <= 0)
                                {
                                    Instantiate(mid_part, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                            if (hit.transform.CompareTag("enemy_low"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().low_healt -= hit_dmg;
                                EnemyKill(1, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().low_healt, low_part, null, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().low_healt <= 0)
                                {
                                    Instantiate(low_part, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                            if (hit.transform.CompareTag("enemy_skill"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().skll_health--;
                                EnemyKill(2, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().skll_health, mid_part, skill_Icon, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().skll_health <= 0)
                                {
                                    Instantiate(mid_part, hit.transform.position, Quaternion.identity);
                                    Instantiate(skill_Icon, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                            if (hit.transform.CompareTag("fast_skill_enemy"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().skll_health--;
                                EnemyKill(2, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().skll_health, mid_part, fast_skill_Icon, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().skll_health <= 0)
                                {
                                    Instantiate(mid_part, hit.transform.position, Quaternion.identity);
                                    Instantiate(fast_skill_Icon, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                            if (hit.transform.CompareTag("range_skill_enemy"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().skll_health--;
                                EnemyKill(2, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().skll_health, mid_part, range_skill_Icon, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().skll_health <= 0)
                                {
                                    Instantiate(mid_part, hit.transform.position, Quaternion.identity);
                                    Instantiate(range_skill_Icon, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                            if (hit.transform.CompareTag("upgrade_skill_enemy"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().skll_health--;
                                EnemyKill(2, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().skll_health, mid_part, upgrade_skill_Icon, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().skll_health <= 0)
                                {
                                    Instantiate(mid_part, hit.transform.position, Quaternion.identity);
                                    Instantiate(upgrade_skill_Icon, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                        }
                    }
                }
            }
            if (!fast_shot)
            {
                if (Input.GetMouseButtonDown(0) && Time.time > hit_time)
                {
                    int lyr = 1 << 8;
                    lyr = ~lyr;
                    hit_time = Time.time + hit_time_2;
                    gun.GetComponent<Animator>().Play("gun_mouse1");
                    shoot.Play();
                    if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, lyr))
                    {
                        if (hit.transform.CompareTag("enemy_boss") || hit.transform.CompareTag("enemy_low") || hit.transform.CompareTag("enemy_mid") || hit.transform.CompareTag("enemy_skill") || hit.transform.CompareTag("fast_skill_enemy") || hit.transform.CompareTag("range_skill_enemy") || hit.transform.CompareTag("upgrade_skill_enemy"))
                        {
                            Instantiate(hit_Part, hit.point, Quaternion.identity);
                            hit_enemy.Play();

                            if (hit.transform.CompareTag("enemy_boss"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().boss_health -= hit_dmg;
                                EnemyKill(1, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().boss_health, boss_part, null, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().boss_health <= 0)
                                {
                                    Instantiate(boss_part, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                            if (hit.transform.CompareTag("enemy_mid"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().mid_health -= hit_dmg;
                                EnemyKill(1, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().mid_health, mid_part, null, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().mid_health <= 0)
                                {
                                    Instantiate(mid_part, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                            if (hit.transform.CompareTag("enemy_low"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().low_healt -= hit_dmg;
                                EnemyKill(1,hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().low_healt, low_part, null, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().low_healt <= 0)
                                {
                                    Instantiate(low_part, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                            if (hit.transform.CompareTag("enemy_skill"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().skll_health--;
                                EnemyKill(2, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().skll_health, mid_part, skill_Icon, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().skll_health <= 0)

                                {
                                    Instantiate(mid_part, hit.transform.position, Quaternion.identity);
                                    Instantiate(skill_Icon, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }

                            if (hit.transform.CompareTag("fast_skill_enemy"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().skll_health--;
                                EnemyKill(2, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().skll_health, mid_part, fast_skill_Icon, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().skll_health <= 0)
                                {
                                    Instantiate(mid_part, hit.transform.position, Quaternion.identity);
                                    Instantiate(fast_skill_Icon, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                            if (hit.transform.CompareTag("range_skill_enemy"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().skll_health--;
                                EnemyKill(2, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().skll_health, mid_part, range_skill_Icon, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().skll_health <= 0)
                                {
                                    Instantiate(mid_part, hit.transform.position, Quaternion.identity);
                                    Instantiate(range_skill_Icon, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }
                            if (hit.transform.CompareTag("upgrade_skill_enemy"))
                            {
                                hit.transform.gameObject.GetComponent<g2_enemy>().skll_health--;
                                EnemyKill(2, hit.transform.gameObject, hit.transform.gameObject.GetComponent<g2_enemy>().skll_health, mid_part, upgrade_skill_Icon, dead_enemy);
                                /*if (hit.transform.gameObject.GetComponent<g2_enemy>().skll_health <= 0)
                                {
                                    Instantiate(mid_part, hit.transform.position, Quaternion.identity);
                                    Instantiate(upgrade_skill_Icon, hit.transform.position, Quaternion.identity);
                                    dead_enemy.Play();
                                    Destroy(hit.transform.gameObject);
                                }*/
                            }

                        }
                    }
                }
            }
        }
       
    }

    public static void EnemyKill(int enemy_index,GameObject enemy,float enemy_heal,GameObject part, GameObject icon, AudioSource for_dead_au)
    {
        
        if (enemy_index == 1)
        {
            if (enemy_heal <= 0)
            {
                print("for low");
                Instantiate(part, enemy.transform.position, Quaternion.identity);
                for_dead_au.Play();
                Destroy(enemy.transform.gameObject);
            }
           
        }

        if (enemy_index == 2)
        {
            if (enemy_heal <= 0)
            {
                print("for skill");
                Instantiate(part, enemy.transform.position, Quaternion.identity);
                Instantiate(icon, enemy.transform.position, Quaternion.identity);
                for_dead_au.Play();
                Destroy(enemy.transform.gameObject);
            }
        }
    }


}












// 12.02.2024 / FÝNALLY FINISH / CONGRATZZZ
// EXDONATELLA - BCGKKY