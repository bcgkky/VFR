using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class homeLoadScene : MonoBehaviour
{
    public Slider slidLoad;

    void Start()
    {
        StartCoroutine(SahneYuklemeAsamasi(forGameSelect.game_Index));
    }

    IEnumerator SahneYuklemeAsamasi(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slidLoad.value = progress;
            yield return null;
        }
    }
}
