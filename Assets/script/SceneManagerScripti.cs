using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScripti : MonoBehaviour
{
    public GameObject anaManu;
    public GameObject bilgilendirmeMenusu0, bilgilendirmeMenusu1, muteBut, unMuteBut, load, VFR_Text;
    public GameObject oyunuYapanlar;
    public Slider slid;


    public void oyununaGiriomKnk(int _sceneIndex)
    {
        StartCoroutine(SahneYuklemeAsamasi(_sceneIndex));
    }
    IEnumerator SahneYuklemeAsamasi(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);
        load.SetActive(true);

 
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slid.value = progress;
            yield return null;
        }
    }
    public void oyundanCiktimYaKnk()
    {
        Application.Quit();
        
    }
    private void Start()
    {
        bilgilendirmeMenusu0.SetActive(false);
        oyunuYapanlar.SetActive(false);
        VFR_Text.SetActive(true);

        PlayerPrefs.SetFloat("vol",1f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
 
    public void bilgilenelimKnk()
    {
        bilgilendirmeMenusu1.SetActive(false);
        anaManu.SetActive(false);
        oyunuYapanlar.SetActive(false);
        bilgilendirmeMenusu0.SetActive(true);
        VFR_Text.SetActive(false);


    }

    public void AnaMenuyeDonelimKnk()
    {
        bilgilendirmeMenusu1.SetActive(false);
        bilgilendirmeMenusu0.SetActive(false);
        oyunuYapanlar.SetActive(false);
        anaManu.SetActive(true);
        VFR_Text.SetActive(true);

    }

    public void oyunuYapanMuhtesemKisiler()
    {

        VFR_Text.SetActive(false);
        bilgilendirmeMenusu1.SetActive(false);
        bilgilendirmeMenusu0.SetActive(false);
        anaManu.SetActive(false);
        oyunuYapanlar.SetActive(true);
    }
    /*public void oynanis()
    {
        VFR_Text.SetActive(false);
        bilgilendirmeMenusu1.SetActive(true);
        bilgilendirmeMenusu0.SetActive(false);
        anaManu.SetActive(false);
        oyunuYapanlar.SetActive(false);
    }*/

    public void _forMute()
    {
        PlayerPrefs.SetFloat("vol", 0f);
        if (PlayerPrefs.HasKey("vol"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("vol");
        }
        muteBut.SetActive(false);
        unMuteBut.SetActive(true);
    } 
    public void _forUnMute()
    {
        PlayerPrefs.SetFloat("vol", 1f);
        if (PlayerPrefs.HasKey("vol"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("vol");
        }
        muteBut.SetActive(true);
        unMuteBut.SetActive(false);
    }
















    public void p1URL()
    {
        Application.OpenURL("https://www.linkedin.com/in/bengisu-karpuzoglu-3a827024a/");
        
    } 
    public void p2URL()
    {
        Application.OpenURL("https://www.linkedin.com/in/burakcan-g%C3%B6kkaya-272626249/");
    }

}
