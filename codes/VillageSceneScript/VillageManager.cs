using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class VillageManager : Singleton<VillageManager>
{
    [SerializeField] TextMeshProUGUI playerLevelText;
    [SerializeField] Slider expSlider;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI playerGold;
    [SerializeField] Image Inventory;


    private void Awake()
    {
        base.Awake();
        Debug.Log("빌리지 Awake");
        LoadVillageData();

    }

    private void Update()
    {
        GameManager.Instance.AddTime();
        GameManager.PlayerDataSave();

    }

    public void LoadVillageData()
    {
        GameManager.CheckPlayerEXP();
        playerLevelText.text = GameManager.playerData.PlayerLevel.ToString();
        expSlider.value = ((float) GameManager.playerData.CurrentEXP / (float) GameManager.playerData.NeedEXPToLevelUP);
        expText.text = GameManager.playerData.CurrentEXP.ToString() + "/" + GameManager.playerData.NeedEXPToLevelUP.ToString() + "(" + (((float)GameManager.playerData.CurrentEXP / (float)GameManager.playerData.NeedEXPToLevelUP)*100).ToString("F2") + "%)";
        playerGold.text = GameManager.playerData.CurrentGold.ToString();
    }

    public void LoadBattleMoveScene()
    {
        SceneManager.LoadScene("BattleMoveScene");

    }

    public void LoadLobbyScene()
    {
        Destroy(GameManager.Instance);
        SceneManager.LoadScene("LobbyScene");
    }

    #region playerInfo
    [SerializeField] Image playerInfoImg;

    [SerializeField] TextMeshProUGUI playerLevelText_Inv;
    [SerializeField] TextMeshProUGUI playerEXPText_Inv;
    [SerializeField] TextMeshProUGUI playerHPText_Inv;
    [SerializeField] TextMeshProUGUI playerMPText_Inv;
    [SerializeField] TextMeshProUGUI playerADText_Inv;
    [SerializeField] TextMeshProUGUI playerAPText_Inv;
    [SerializeField] TextMeshProUGUI playerDefensive_ADText_Inv;
    [SerializeField] TextMeshProUGUI playerDefensive_APText_Inv;
    [SerializeField] TextMeshProUGUI playerSTRText_Inv;
    [SerializeField] TextMeshProUGUI playerDEXText_Inv;
    [SerializeField] TextMeshProUGUI playerINTText_Inv;
    [SerializeField] TextMeshProUGUI playerLUKText_Inv;

    public void LoadPlayerInfoData()
    {
        playerInfoImg.gameObject.LeanScale(Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutQuad);

        GameManager.CheckPlayerEXP();

        playerLevelText_Inv.text = GameManager.playerData.PlayerLevel.ToString();
        playerEXPText_Inv.text = GameManager.playerData.CurrentEXP.ToString() + "/" + GameManager.playerData.NeedEXPToLevelUP;
        playerHPText_Inv.text = GameManager.playerData.PlayerCurrentHP.ToString() + "/" + GameManager.playerData.PlayerHP;
        playerMPText_Inv.text = GameManager.playerData.PlayerCurrentMP.ToString() + "/" + GameManager.playerData.PlayerMP;
        playerADText_Inv.text = GameManager.playerData.PlayerAD.ToString();
        playerAPText_Inv.text = GameManager.playerData.PlayerAP.ToString();
        playerDefensive_ADText_Inv.text = GameManager.playerData.PlayerDefensive_AD.ToString();
        playerDefensive_APText_Inv.text = GameManager.playerData.PlayerDefensive_AP.ToString();
        playerSTRText_Inv.text = GameManager.playerData.PlayerStrength.ToString();
        playerDEXText_Inv.text = GameManager.playerData.PlayerAgility.ToString();
        playerINTText_Inv.text = GameManager.playerData.PlayerMagicPower.ToString();
        playerLUKText_Inv.text = GameManager.playerData.PlayerLuck.ToString();
    }

    public void InfoExit()
    {
        playerInfoImg.gameObject.LeanScale(Vector3.zero, 0.7f).setEase(LeanTweenType.easeInElastic);
    }

    #endregion
}
