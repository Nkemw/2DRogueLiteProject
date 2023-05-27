using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LoadingManager : MonoBehaviour
{
    [SerializeField] Slider loadingSlider;


    private void Awake()
    {
        loadingSlider.value = 0;
        StartCoroutine("LoadAsyncScene");
    }


    [SerializeField] private float loadTime;
    private float currentTime = 0;
    IEnumerator LoadAsyncScene()
    {
        yield return new WaitForSeconds(0.05f);
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync("VillageScene");
        asyncScene.allowSceneActivation = false;
        

        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime;

            loadingSlider.value = Mathf.Lerp(0, 1, currentTime / loadTime);

            if(loadingSlider.value == 1f)
            {
                asyncScene.allowSceneActivation = true;
            }
        }
    }
}
