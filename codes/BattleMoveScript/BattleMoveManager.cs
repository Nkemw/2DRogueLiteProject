using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleMoveManager : MonoBehaviour
{
    [SerializeField] Slider progress;
    [SerializeField] Image img;
    [SerializeField] float fadingTime = 1f;

    private bool battleTrigger = false;

    private float triggerTime = 0f;
    void Update()
    {

        if (battleTrigger)
        {
            StartCoroutine(FadeIn());
        } else
        {
            GameManager.Instance.AddTime();
            GameManager.Instance.AddStageProgress();

            progress.value = GameManager.playerData.stageProgress;

            triggerTime += Time.deltaTime;

            if (triggerTime >= 1f)
            {
                GameManager.PlayerDataSave();
                if (RandomManager.CheckWeight(10))
                {
                    battleTrigger = true;
                }
                else
                {
                    battleTrigger = false;
                    triggerTime -= 1f;
                }
            }
        }
    }

    public void LoadBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    
    IEnumerator FadeIn()
    {
        yield return null;


        Color imgAlpha = img.color;

        float currentTime = 0f;

        while (true)
        {
            yield return null;

            currentTime += Time.deltaTime;
            if (currentTime >= fadingTime)
            {
                break;
            }
            else
            {
                imgAlpha.a = Mathf.Lerp(0f, 1f, currentTime / fadingTime);
                img.color = imgAlpha;
            }
        }
        LoadBattleScene();
        StopCoroutine(FadeIn());

    }
}
