using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData{
    public int PlayerHP = 100;
    public int PlayerCurrentHP;
    public int PlayerMP = 100;
    public int PlayerCurrentMP;
    public int PlayerAD = 5;
    public int PlayerAP = 5;
    public int PlayerDefensive_AD = 5;
    public int PlayerDefensive_AP = 5;
    public int PlayerStrength = 5;
    public int PlayerAgility = 5;
    public int PlayerMagicPower = 5;
    public int PlayerLuck = 5;
    public string PlayerBaseAttack = "BaseAttack";
    public string PlayerSkill1;
    public string PlayerSkill2;
    public string PlayerSkill3;
    public int NeedEXPToLevelUP = 500;
    public int PlayerLevel = 1;
    public int CurrentGold = 0;
    public int CurrentEXP = 0;
    public int ReinforceStoneAmount = 0;
    public float time = 0f;
    public float stageProgress = 0f;
}

public enum SceneName
{
    LobbyScene,
    VillageScene,
    BattleScene
}
public class GameManager : Singleton<GameManager>
{
    public GameData gameExcelData;

    //public RandomManager randomManager = new RandomManager();

    public static PlayerData playerData = new PlayerData();

    public float time;
    public int monsterKillScore;

    public int background;
    
    private void Awake()
    {
        base.Awake();

        gameExcelData = Resources.Load<GameData>("GameData");

        //PlayerDataSave();
        PlayerDataLoad();
        Debug.Log(Application.persistentDataPath);
    }

    public static void PlayerDataCheck()
    {
        if(File.Exists(Application.persistentDataPath + "/playerData.json"))
        {
            PlayerDataLoad();
        } else
        {
            PlayerDataInit();
        }
    }

    public static void PlayerDataInit()
    {
        playerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(Application.persistentDataPath + "/playerAccountData.json"));

        PlayerDataSave();
    }

    public static void PlayerDataSave()
    {

        File.WriteAllText(Application.persistentDataPath + "/playerData.json", JsonUtility.ToJson(playerData));
        Debug.Log(Application.persistentDataPath);
    }

    public static void PlayerDataLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.json")) {
            playerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(Application.persistentDataPath + "/playerData.json"));

            GameManager.Instance.time = playerData.time;
        }

        //playerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(Application.persistentDataPath + "/playerData.json"));
    }

    public static void PlayerDataDelete()
    {
        File.Delete(Application.persistentDataPath + "/playerData.json");
    }

    public static void CheckPlayerEXP()
    {
        if (playerData.CurrentEXP >= playerData.NeedEXPToLevelUP)
        {
            playerData.PlayerLevel++;
            playerData.CurrentEXP -= playerData.NeedEXPToLevelUP;
            playerData.NeedEXPToLevelUP += 500;

            playerData.PlayerHP += 10;
            playerData.PlayerMP += 10;
            playerData.PlayerCurrentHP += 10;
            playerData.PlayerCurrentMP += 10;

            playerData.PlayerAD += 1;
            playerData.PlayerAP += 1;
            playerData.PlayerDefensive_AD += 1;
            playerData.PlayerDefensive_AP += 1;
            playerData.PlayerStrength += 1;
            playerData.PlayerAgility += 1;
            playerData.PlayerMagicPower += 1;
            playerData.PlayerLuck += 1;
        }
    }

    public void TimeReset()
    {
        time = 0f;
    }

    public void AddTime()
    {
        time += Time.deltaTime;
        playerData.time = time;
        Debug.Log(time);
    }

    public void AddStageProgress()
    {
        playerData.stageProgress += Time.deltaTime/100f;
    }
}
