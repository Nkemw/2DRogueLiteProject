using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestInteract : MonoBehaviour, UIActive
{
    [SerializeField] Image restPopup;
    [SerializeField] TextMeshProUGUI playerChangableHPText;

    [SerializeField] Button exitBtn;
    [SerializeField] Button openBtn;

    public IEnumerator UIClose()
    {
        float time = 0f;
        restPopup.gameObject.LeanScale(Vector3.zero, 0.3f).setEase(LeanTweenType.easeInOutQuad);

        while (true)
        {
            time += Time.deltaTime;
            if(time  >= 0.7f)
            {
                break;
            }
            yield return null;
        }

        openBtn.enabled = true;
        exitBtn.enabled = true;

        gameObject.SetActive(false);


        StopCoroutine(UIClose());
    }

    public void StartUIClose()
    {
        if (exitBtn.enabled && openBtn.enabled)
        {
            openBtn.enabled = false;
            exitBtn.enabled = false;
            StartCoroutine(UIClose());
        }
    }

    public void UIOpen()
    {
        gameObject.SetActive(true);

        restPopup.gameObject.LeanScale(Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutQuad);

        playerChangableHPText.text = "<color=red>" + GameManager.playerData.PlayerCurrentHP + "</color><color=black> -> </color>" + GameManager.playerData.PlayerHP;
    }

    #region Fading
    [SerializeField] Image fadeImg;

    public void RecoveryHP()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        fadeImg.gameObject.SetActive(true);

        float currentTime = 0f;
        float EndTime = 1f;

        Color alphaColor = fadeImg.color;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= EndTime)
            {
                break;
            }

            alphaColor.a = Mathf.Lerp(0f, 1f, currentTime / EndTime);
            fadeImg.color = alphaColor;

            yield return null;
        }

        GameManager.playerData.PlayerCurrentHP = GameManager.playerData.PlayerHP;
        playerChangableHPText.text = "<color=red>" + GameManager.playerData.PlayerCurrentHP + "</color><color=black> -> </color>" + GameManager.playerData.PlayerHP;
        GameManager.Instance.time += 60f;

        StartCoroutine(FadeOut());
        StopCoroutine(FadeIn());
    }

    public IEnumerator FadeOut()
    {
        float currentTime = 0f;
        float EndTime = 1f;

        Color alphaColor = fadeImg.color;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= EndTime)
            {
                break;
            }

            alphaColor.a = Mathf.Lerp(1f, 0f, currentTime / EndTime);
            fadeImg.color = alphaColor;

            yield return null;
        }

        

        fadeImg.gameObject.SetActive(false);
        StopCoroutine(FadeOut());
    }
    #endregion
}
