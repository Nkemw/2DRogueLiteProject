using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ClockRotate : MonoBehaviour
{
    [SerializeField] private GameObject clockPointer;
    [SerializeField] private TextMeshProUGUI dayText;

    private int dayCount = 1;

    void Update()
    {
        clockPointer.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -2f * GameManager.Instance.time));
        
        if(GameManager.Instance.time / 180f >= dayCount)
        {
            dayCount++;
            dayText.text = dayCount.ToString() + "ÀÏÂ÷";
        }
        //clockPointer.transform.Rotate(new Vector3(0f, 0f, -* -2f));
    }
}
