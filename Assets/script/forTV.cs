using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class forTV : MonoBehaviour
{
    RaycastHit hit;
    bool tvOpen;
    bool canChangeCH = false;
    float waiting = 0f;
    float waitingChangeCH = 0f;
    public GameObject tvPressInfo;
    public GameObject tv, LCD_Cube, TV_Player, TV_PlayerCH2, CH_Panel;
    float volumeVal = 1f;
    public Slider tvVolSlid;


    private void FixedUpdate()
    {

        float dist = Vector3.Distance(transform.position, tv.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray,out hit, 100))
        {
            if (dist < 7 && hit.transform.CompareTag("TV") && !GameObject.FindGameObjectWithTag("Player").GetComponent<forPlayer>().thatVFRbox)
            {
                tvPressInfo.SetActive(true);
                if (Input.GetMouseButton(0) && Time.time > waiting)
                {
                    waiting = +Time.time + 3f;
                    tvOpen = !tvOpen;

                    if (tvOpen)
                    {
                        hit.transform.gameObject.GetComponentInParent<Animator>().Play("TV");
                        Invoke("tvOpening", 2.5f);
                    }
                    else
                    {
                        hit.transform.gameObject.GetComponentInParent<Animator>().Play("TVturnOff");
                        GameObject.FindGameObjectWithTag("homeCanvas").GetComponent<AudioSource>().enabled = true;
                        LCD_Cube.SetActive(false);
                        TV_Player.GetComponent<VideoPlayer>().Stop();
                        TV_PlayerCH2.GetComponent<VideoPlayer>().Stop();
                        canChangeCH = false;
                        CH_Panel.SetActive(false);
                    }
                }
            }
            if (dist > 7 || !hit.transform.CompareTag("TV") || GameObject.FindGameObjectWithTag("Player").GetComponent<forPlayer>().thatVFRbox)
            {
                tvPressInfo.SetActive(false);
            }
        }
        if (canChangeCH)
        {
            CH_Panel.SetActive(true);
            if (Input.GetKey(KeyCode.Alpha1) && Time.time > waitingChangeCH)
            {
                waitingChangeCH = Time.time + 1f;
                TV_Player.GetComponent<VideoPlayer>().Play();
                TV_PlayerCH2.GetComponent<VideoPlayer>().Stop();
            }
            if (Input.GetKey(KeyCode.Alpha2) && Time.time > waitingChangeCH)
            {
                waitingChangeCH = Time.time + 1f;
                TV_Player.GetComponent<VideoPlayer>().Stop();
                TV_PlayerCH2.GetComponent<VideoPlayer>().Play();
            }
            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                tvVolSlid.value += .01f;
                TV_Player.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, tvVolSlid.value);
                TV_PlayerCH2.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, tvVolSlid.value);
            }
            if (Input.GetKey(KeyCode.KeypadMinus) || Input.GetKey("-"))
            {
                tvVolSlid.value -= .01f;
                TV_Player.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, tvVolSlid.value);
                TV_PlayerCH2.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, tvVolSlid.value);
            }
        }
    }


    void tvOpening()
    {
        canChangeCH = true;
        LCD_Cube.SetActive(true);
        TV_Player.GetComponent<VideoPlayer>().Play();
        TV_PlayerCH2.GetComponent<VideoPlayer>().Stop();
        GameObject.FindGameObjectWithTag("homeCanvas").GetComponent<AudioSource>().enabled = false;
    }
}
