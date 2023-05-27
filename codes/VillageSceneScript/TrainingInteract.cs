using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainingInteract : MonoBehaviour, UIActive
{
    [SerializeField] Image trainingPopup;

    [SerializeField] Button openBtn;
    [SerializeField] Button exitBtn;

    public IEnumerator UIClose()
    {
        float time = 0f;
        trainingPopup.gameObject.LeanScale(Vector3.zero, 0.3f).setEase(LeanTweenType.easeInOutQuad);

        while (true)
        {
            time += Time.deltaTime;
            if (time >= 0.7f)
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

        trainingPopup.gameObject.LeanScale(Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutQuad);

    }

    public int trainingType;
    public void StrengthTrainingBtnClicked()
    {
        trainingType = (int)TrainingType.Strength;
        StartTraining();
    }

    public void AgilityTrainingBtnClicked()
    {
        trainingType = (int)TrainingType.Agility;
        StartTraining();
    }

    public void MagicPowerTrainingBtnClicked()
    {
        trainingType = (int)TrainingType.MagicPower;
        StartTraining();
    }

    public void LuckTrainingBtnClicked()
    {
        trainingType = (int)TrainingType.Luck;
        StartTraining();
    }

    [SerializeField] Image resultPopup;
    [SerializeField] TextMeshProUGUI resultText;
    public void ResultPopupOpen()
    {
        resultPopup.gameObject.SetActive(true);
    }

    public void ResultPopupclose()
    {
        trainingPopup.gameObject.LeanScale(Vector3.zero, 0.1f).setEase(LeanTweenType.easeInOutQuad);
        resultPopup.gameObject.SetActive(false);
    }
    #region [Fading]
    [SerializeField] Image fadeImg;

    public void StartTraining()
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
        #region [TrainingResult]

        resultText.text = "";
        ResultPopupOpen();
        switch (trainingType)
        {
            case (int)TrainingType.Strength:
                //int upgradeAmount = 0;

                int upgradeValue;
                for(int i = 0; i < 4; i++)
                {
                    

                    if (i == 0)  //HP ��ȭ
                    {
                        upgradeValue = RandomManager.CreateRandomByValue(11);
                        upgradeValue--;

                        if (!(upgradeValue == 0))    //������� 1 �̻��̸� �� ����
                        {
                            resultText.text += "HP " + GameManager.playerData.PlayerHP + " -> <color=green>" + (GameManager.playerData.PlayerHP + upgradeValue) + "</color>\t\t";
                        } else
                        {
                            resultText.text += "HP " + GameManager.playerData.PlayerHP + " -> " + GameManager.playerData.PlayerHP + "\t";
                        }

                        GameManager.playerData.PlayerHP += upgradeValue;
                    }
                    else //���ݷ�, �ٷ�, ���� �ɷ�ġ ��ȭ
                    {
                        upgradeValue = RandomManager.CreateRandomByValue(2);
                        upgradeValue--;

                        if (i == 1) 
                        {
                            if (!(upgradeValue == 0))  
                            {
                                resultText.text += "���ݷ� " + GameManager.playerData.PlayerAD + " -> <color=green>" + (GameManager.playerData.PlayerAD + upgradeValue) + "</color>\n";
                            }
                            else
                            {
                                resultText.text += "���ݷ� " + GameManager.playerData.PlayerAD + " -> " + GameManager.playerData.PlayerAD + "\n";
                            }

                            GameManager.playerData.PlayerAD += upgradeValue;

                        } else if(i == 2)
                        {
                            if (!(upgradeValue == 0))    
                            {
                                resultText.text += "�ٷ� " + GameManager.playerData.PlayerStrength + " -> <color=green>" + (GameManager.playerData.PlayerStrength + upgradeValue) + "</color>\t\t";
                            }
                            else
                            {
                                resultText.text += "�ٷ� " + GameManager.playerData.PlayerStrength + " -> " + GameManager.playerData.PlayerStrength + "\t\t";
                            }

                            GameManager.playerData.PlayerStrength += upgradeValue;

                        } else if(i == 3)
                        {
                            if (!(upgradeValue == 0))
                            {
                                resultText.text += "���� " + GameManager.playerData.PlayerDefensive_AD + " -> <color=green>" + (GameManager.playerData.PlayerDefensive_AD + upgradeValue) + "</color>";
                            }
                            else
                            {
                                resultText.text += "���� " + GameManager.playerData.PlayerDefensive_AD + " -> " + GameManager.playerData.PlayerDefensive_AD;
                            }

                            GameManager.playerData.PlayerDefensive_AD += upgradeValue;
                        }
                    }
                }
                break;
            case (int)TrainingType.Agility:
                upgradeValue = RandomManager.CreateRandomByValue(2);
                upgradeValue--;

                if (!(upgradeValue == 0))   
                {
                    resultText.text += "��ø " + GameManager.playerData.PlayerAgility + " -> <color=green>" + (GameManager.playerData.PlayerAgility + upgradeValue) + "</color>";
                }
                else
                {
                    resultText.text += "��ø " + GameManager.playerData.PlayerAgility + " -> " + GameManager.playerData.PlayerAgility;
                }

                GameManager.playerData.PlayerAgility += upgradeValue;
                
                break;
            case (int)TrainingType.MagicPower:

                for (int i = 0; i < 4; i++)
                {

                    if (i == 0)  //MP ��ȭ
                    {
                        upgradeValue = RandomManager.CreateRandomByValue(11);
                        upgradeValue--;

                        if (!(upgradeValue == 0))    
                        {
                            resultText.text += "MP " + GameManager.playerData.PlayerMP + " -> <color=green>" + (GameManager.playerData.PlayerMP + upgradeValue) + "</color>\t";
                        }
                        else
                        {
                            resultText.text += "MP " + GameManager.playerData.PlayerMP + " -> " + GameManager.playerData.PlayerMP + "\t";
                        }

                        GameManager.playerData.PlayerMP += upgradeValue;
                    }
                    else //�������ݷ�, ����, �������� �ɷ�ġ ��ȭ
                    {
                        upgradeValue = RandomManager.CreateRandomByValue(2);
                        upgradeValue--;

                        if (i == 1)
                        {
                            if (!(upgradeValue == 0))
                            {
                                resultText.text += "�������ݷ� " + GameManager.playerData.PlayerAP + " -> <color=green>" + (GameManager.playerData.PlayerAP + upgradeValue) + "</color>\n";
                            }
                            else
                            {
                                resultText.text += "�������ݷ� " + GameManager.playerData.PlayerAP + " -> " + GameManager.playerData.PlayerAP + "\n";
                            }

                            GameManager.playerData.PlayerAP += upgradeValue;

                        }
                        else if (i == 2)
                        {
                            if (!(upgradeValue == 0))
                            {
                                resultText.text += "���� " + GameManager.playerData.PlayerMagicPower + " -> <color=green>" + (GameManager.playerData.PlayerMagicPower + upgradeValue) + "</color>\t\t";
                            }
                            else
                            {
                                resultText.text += "���� " + GameManager.playerData.PlayerMagicPower + " -> " + GameManager.playerData.PlayerMagicPower + "\t\t";
                            }

                            GameManager.playerData.PlayerMagicPower += upgradeValue;

                        }
                        else if (i == 3)
                        {
                            if (!(upgradeValue == 0))
                            {
                                resultText.text += "�������� " + GameManager.playerData.PlayerDefensive_AP + " -> <color=green>" + (GameManager.playerData.PlayerDefensive_AP + upgradeValue) + "</color>";
                            }
                            else
                            {
                                resultText.text += "�������� " + GameManager.playerData.PlayerDefensive_AP + " -> " + GameManager.playerData.PlayerDefensive_AP;
                            }

                            GameManager.playerData.PlayerDefensive_AP += upgradeValue;
                        }
                    }
                }
                break;
            case (int)TrainingType.Luck:
                upgradeValue = RandomManager.CreateRandomByValue(2);
                upgradeValue--;

                if (!(upgradeValue == 0))
                {
                    resultText.text += "��� " + GameManager.playerData.PlayerLuck + " -> <color=green>" + (GameManager.playerData.PlayerLuck + upgradeValue) + "</color>";
                }
                else
                {
                    resultText.text += "��� " + GameManager.playerData.PlayerLuck + " -> " + GameManager.playerData.PlayerLuck;
                }

                GameManager.playerData.PlayerLuck += upgradeValue;
                break;
        }

        GameManager.Instance.time += 60f;

        GameManager.PlayerDataSave();

        #endregion
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
