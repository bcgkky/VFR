using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forGameSelect : MonoBehaviour
{

    public static int game_Index;
    public GameObject loadScenePanel, game1Info_Panel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           
            game_Index = 2;
            game1Info_Panel.SetActive(true);
            gameObject.SetActive(false);
            loadScenePanel.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            game_Index = 3;
            gameObject.SetActive(false);
            loadScenePanel.SetActive(true);
        }
        
        
        if (this.CompareTag("game1_info_panel"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                loadScenePanel.SetActive(true);
                gameObject.SetActive(false);
            }
        }

    }
}
