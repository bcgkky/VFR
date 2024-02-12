using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class g2_spawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemys;
    [SerializeField]
    GameObject boss, skill_enemy, level_Panel, finish_Panel, fast_skill_enemy, range_skill_enemy, upgrage_skill_enemy, info_panel;


    public static bool game_finish = false;
    public static bool info_ = false;
    public static bool _isRead = false;
    bool _forCanPress = false;

    int enemy_Index;
    float level1 = 10;
    float level2 = 20;
    float level3 = 30;
    float level4 = 40;
    float level5 = 50;

    public AudioSource new_level;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>().enabled = false;
        if (!info_)
        {
            Invoke("_forInfo", 2f);
        }
        else StartCoroutine(Spawn());

        Cursor.visible = false;
    }

    private void Update()
    {
        if (!info_)
        {
            if (_forCanPress)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    info_ = true;
                    StartCoroutine(Spawn());
                    print("spacee");
                }
            }
            
        }
    }
    IEnumerator Spawn()
    {
        info_panel.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>().enabled = true;
        yield return new WaitForSeconds(2f);
        if (info_)
        {
            yield return new WaitForSeconds(2f);
            level_Panel.SetActive(true);
            new_level.Play();
            level_Panel.GetComponentInChildren<Text>().text = "LEVEL 1";
            gameObject.GetComponent<AudioSource>().Play();
            while (level1 != 0)
            {
                yield return new WaitForSeconds(4);
                level_Panel.SetActive(false);
                level1--;
                enemy_Index = Random.Range(0, 2);

                Instantiate(enemys[enemy_Index], transform.position, Quaternion.identity);
                if (level1 == 4)
                {
                    Instantiate(boss, transform.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(2f);
            level_Panel.SetActive(true);
            new_level.Play();
            level_Panel.GetComponentInChildren<Text>().text = "LEVEL 2";
            yield return new WaitForSeconds(6f);

            while (level2 != 0)
            {
                yield return new WaitForSeconds(1.5f);
                level_Panel.SetActive(false);
                level2--;
                enemy_Index = Random.Range(0, 2);

                Instantiate(enemys[enemy_Index], transform.position, Quaternion.identity);
                if (level2 == 17 || level2 == 12 || level2 == 4)
                {
                    Instantiate(boss, transform.position, Quaternion.identity);
                }
                if (level2 == 18)
                {
                    Instantiate(fast_skill_enemy, transform.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(2f);
            level_Panel.SetActive(true);
            new_level.Play();
            level_Panel.GetComponentInChildren<Text>().text = "LEVEL 3";
            yield return new WaitForSeconds(6f);

            while (level3 != 0)
            {
                yield return new WaitForSeconds(1f);
                level_Panel.SetActive(false);
                level3--;
                enemy_Index = Random.Range(0, 2);

                Instantiate(enemys[enemy_Index], transform.position, Quaternion.identity);
                if (level3 == 6 || level3 == 3 || level3 == 25 || level3 == 15)
                {
                    Instantiate(boss, transform.position, Quaternion.identity);
                }
                if (level3 == 29)
                {
                    Instantiate(skill_enemy, transform.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(2f);
            level_Panel.SetActive(true);
            new_level.Play();
            level_Panel.GetComponentInChildren<Text>().text = "LEVEL 4";
            yield return new WaitForSeconds(6f);

            while (level4 != 0)
            {
                yield return new WaitForSeconds(1f);
                level_Panel.SetActive(false);
                level4--;
                enemy_Index = Random.Range(0, 2);

                Instantiate(enemys[enemy_Index], transform.position, Quaternion.identity);

                if (level4 == 3 || level4 == 10 || level4 == 21 || level4 == 35 || level4 == 28)
                {
                    Instantiate(boss, transform.position, Quaternion.identity);
                }
                if (level4 == 39)
                {
                    Instantiate(range_skill_enemy, transform.position, Quaternion.identity);
                }

            }

            yield return new WaitForSeconds(2f);
            level_Panel.SetActive(true);
            new_level.Play();
            level_Panel.GetComponentInChildren<Text>().text = "LEVEL 5";
            yield return new WaitForSeconds(6f);

            while (level5 != 0)
            {
                yield return new WaitForSeconds(.65f);
                level_Panel.SetActive(false);
                level5--;
                enemy_Index = Random.Range(0, 2);

                Instantiate(enemys[enemy_Index], transform.position, Quaternion.identity);

                if (level5 == 3 || level5 == 10 || level5 == 21 || level5 == 35 || level5 == 28)
                {
                    Instantiate(boss, transform.position, Quaternion.identity);
                }
                if (level5 == 49)
                {
                    Instantiate(upgrage_skill_enemy, transform.position, Quaternion.identity);
                }
            }

            if (!g2_player.game_over)
            {
                yield return new WaitForSeconds(15f);
                game_finish = true;
                level_Panel.SetActive(true);
                level_Panel.GetComponentInChildren<Text>().text = "KULELERÝ KORUDUN!";
                yield return new WaitForSeconds(3f);

                yield return new WaitForSeconds(1f);
                level_Panel.SetActive(false);

                Invoke("_forSceneControl", 3.8f);
                finish_Panel.SetActive(true);
                GameObject.FindGameObjectWithTag("gameMusic").GetComponent<AudioSource>().Stop();
                finish_Panel.GetComponent<Animator>().Play("finishPanelAnim");
                GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>().enabled = false;
                Invoke("_forTimeScale", 4f);
            }
        }
        
        
    }
    void _forTimeScale()
    {
        Time.timeScale = 0;
    }
    void _forSceneControl()
    {
        GameObject.FindGameObjectWithTag("forScene").GetComponent<gameSceneControl>().enabled = true;
    }
    void _forInfo()
    {
        info_panel.SetActive(true);
        _forCanPress = true;

    }


}
