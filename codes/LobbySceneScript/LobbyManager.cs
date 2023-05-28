using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;


public class LobbyManager : MonoBehaviour
{
    //게임 전체적으로 사용하는 배경음악 리스트
    [SerializeField] List<AudioSource> audioList;

    //배경음악의 소리 크기 관련
    [SerializeField] Slider bgmSlider;
    [SerializeField] TextMeshProUGUI bgmValueText;

    //옵션팝업 active 관련
    [SerializeField] Image optionPopup;
    private bool optionPopupIsActive;

    //계정 생성 관련
    [SerializeField] TMP_InputField accountName;
    [SerializeField] Image accountImg;

    //초월석
    [SerializeField] TextMeshProUGUI reinforceStoneText;

    private void Awake()
    {
        CheckAccount();
        audioList[0].Play();
        audioList[0].volume = 0.05f;

        GameManager.Instance.TimeReset();
        LoadReinforceStoneData();
    }

    public void BGMValueChange()
    {
        bgmValueText.text = (Mathf.Round(bgmSlider.value*100)).ToString();
        audioList[0].volume = bgmSlider.value;
    }

    public void BGMPopupActiveChange()
    {
        optionPopupIsActive = !optionPopupIsActive;
        optionPopup.gameObject.SetActive(optionPopupIsActive);
    }

    public void LoadVillageScene()
    {
        GameManager.PlayerDataCheck();
        SceneManager.LoadScene("LoadingScene");
    }

    public void CheckAccount()
    {
        if (!File.Exists(Application.persistentDataPath + "/playerAccountData.json"))
        {
            accountImg.gameObject.SetActive(true);
        }
    }


    public void AccountCreate()
    {
        PlayerData playerAccountData = new PlayerData();

        playerAccountData.PlayerHP += RandomManager.ApplyFixedRatioValue(10);
        playerAccountData.PlayerCurrentHP = playerAccountData.PlayerHP;
        playerAccountData.PlayerMP += RandomManager.ApplyFixedRatioValue(10);
        playerAccountData.PlayerCurrentMP = playerAccountData.PlayerMP;
        playerAccountData.PlayerAD += RandomManager.ApplyFixedRatioValue(2);
        playerAccountData.PlayerAP += RandomManager.ApplyFixedRatioValue(2);
        playerAccountData.PlayerDefensive_AD += RandomManager.ApplyFixedRatioValue(2);
        playerAccountData.PlayerDefensive_AP += RandomManager.ApplyFixedRatioValue(2);
        playerAccountData.PlayerStrength += RandomManager.ApplyFixedRatioValue(2);
        playerAccountData.PlayerAgility += RandomManager.ApplyFixedRatioValue(2);
        playerAccountData.PlayerMagicPower += RandomManager.ApplyFixedRatioValue(2);
        playerAccountData.PlayerLuck += RandomManager.ApplyFixedRatioValue(2);

        File.WriteAllText(Application.persistentDataPath + "/playerAccountData.json", JsonUtility.ToJson(playerAccountData));

        accountImg.gameObject.SetActive(false);
    }

    public void ReinforcePopupOpen()
    {
        Debug.Log("asd");
    }

    public void LoadReinforceStoneData()
    {
        reinforceStoneText.text = GameManager.playerData.ReinforceStoneAmount.ToString();
    }
}
