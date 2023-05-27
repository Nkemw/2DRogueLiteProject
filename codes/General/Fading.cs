using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] float fadingTime = 5f;
    private void Awake()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        yield return null;


        Color imgAlpha = img.color;

        float currentTime = 0f;

        while (true)
        {
            yield return null;

            currentTime += Time.deltaTime;
            if(currentTime >= fadingTime)
            {
                break;
            }
            else
            {
                imgAlpha.a = Mathf.Lerp(1f, 0f, currentTime / fadingTime);
                img.color = imgAlpha;
            }
        }
        StopCoroutine("FadeOut");

    }
}
