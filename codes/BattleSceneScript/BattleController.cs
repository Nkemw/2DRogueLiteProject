using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Spine.Unity;

public enum Stage1_Monsters
{
    Slime,
    EliteSlime,
    Rabbit,
    EliteRabbit,
    Mushroom,
    EliteMushroom,
    Snail,
    EliteSnail,
    Orc,
    EliteOrc
}

public enum MonsterAnimState
{
    Attack,
    Dead,
    Idle,
    Walk
}
public class BattleController : Singleton<BattleController>
{
    [SerializeField] public List<GameObject> monsterList;// = new List<GameObject>();
    [SerializeField] public Image gameOverImg;
    [SerializeField] public Image playerRewardImg;

    public Button[] skillBtn;
    public List<string> getItems;
    public int totalEXP = 0;
    public int totalGold = 0;

    public int monsterActiveCount = 0;

    public int monsterSpawnCount;      //몬스터 스폰 마릿수
    private int monsterMaxCount = 3;    //몬스터 최대 스폰 마릿수

    public static bool isPlayerDie = false;

    public bool isPlayerAttack = false;

    // Start is called before the first frame update
    void Awake()
    {
        MonsterInit();

        MonsterSpawn();

        PlayerSpawn();

        for(int i = 0; i < skillBtn.Length; i++)
        {
            skillBtn[i].onClick.AddListener(() => ShowSelectableMonster(i));
        }
    }

    public void MonsterSpawn() {

        //monsterList.Clear();

        SetMonsterSpawnCount();

        //SetMonsterActiveTrue();

        SetMonsterObj();

        
        
    }

    public void SetMonsterActiveTrue()
    {
        switch (monsterSpawnCount)
        {
            case 1:
                monsterList[2].SetActive(true);
                break;
            case 2:
                monsterList[1].SetActive(true);
                monsterList[3].SetActive(true);
                break;
            default:
                monsterList[0].SetActive(true);
                monsterList[2].SetActive(true);
                monsterList[4].SetActive(true);
                break;
        }
    }

    public void SetMonsterActiveFalse()
    {
        foreach(GameObject obj in monsterList)
        {
            obj.SetActive(false);
        }
    }

    public void SetMonsterActiveFalse(int index)
    {
        monsterList[index].SetActive(false);
    }

    private void MonsterInit()
    {
        //GameObject t_hpbar = Instantiate(HPBar, EnemyList[i].transform.position, Quaternion.identity, transform);

        for (int i = 0; i < monsterList.Count; i++)
        {
            monsterList[i] = Instantiate(monsterList[i], gameObject.transform);
           // monsterList[i].SetActive(false);
        }

        for (int i = 0; i < 2; i++)
        {
            monsterList[1 + i * 2].transform.position = new Vector3(3.75f + i * 2.5f, -3.35f, 0f);
            monsterList[1 + i * 2].transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        for (int i = 0; i < 3; i++)
        {
            monsterList[0 + i * 2].transform.position = new Vector3(2.5f + i * 2.5f, -3.35f, 0f);
            monsterList[0 + i * 2].transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }

        //2.5 3.75 5 6.25 7.5
    }

    private void SetMonsterSpawnCount()
    {
        monsterSpawnCount = RandomManager.CreateMonsterSpawnCount(monsterMaxCount);
        monsterActiveCount = monsterSpawnCount;
        Debug.Log("몬스터 카운트: " + monsterSpawnCount);
    }

    public TableEntity_MonsterData DeepCopy(int index, List<TableEntity_MonsterData> monsterData)
    {
        TableEntity_MonsterData newGameData = new TableEntity_MonsterData();
        //uid, name, monsterhp, mp, ad, ap, ad, ap str, agil, magic, luck base, 1,2,3, exp, dropitem, dropgold, dropexp, elite, stamina, power, stat

        newGameData.UID = monsterData[index].UID;
        newGameData.MonsterName = monsterData[index].MonsterName;
        newGameData.MonsterHP = monsterData[index].MonsterHP;
        newGameData.MonsterMP = monsterData[index].MonsterMP;
        newGameData.MonsterAD= monsterData[index].MonsterAD;
        newGameData.MonsterAP = monsterData[index].MonsterAP;
        newGameData.MonsterDefensive_AD = monsterData[index].MonsterDefensive_AD;
        newGameData.MonsterDefensive_AP = monsterData[index].MonsterDefensive_AP;
        newGameData.MonsterStrength = monsterData[index].MonsterStrength;
        newGameData.MonsterAgility = monsterData[index].MonsterAgility;
        newGameData.MonsterMagicPower = monsterData[index].MonsterMagicPower;
        newGameData.MonsterLuck = monsterData[index].MonsterLuck;
        newGameData.MonsterBaseAttack = monsterData[index].MonsterBaseAttack;
        newGameData.MonsterSkill1 = monsterData[index].MonsterSkill1;
        newGameData.MonsterSkill2 = monsterData[index].MonsterSkill2;
        newGameData.MonsterSkill3 = monsterData[index].MonsterSkill3;
        newGameData.MonsterEXP = monsterData[index].MonsterEXP;
        newGameData.MonsterDropItem = monsterData[index].MonsterDropItem;
        newGameData.MonsterDropGold = monsterData[index].MonsterDropGold;
        newGameData.MonsterDropEXP = monsterData[index].MonsterDropEXP;
        newGameData.EliteMonsterRate = monsterData[index].EliteMonsterRate;
        newGameData.MonsterStaminaRandomeChangeValue = monsterData[index].MonsterStaminaRandomeChangeValue;
        newGameData.MonsterPowerRandomChangeValue = monsterData[index].MonsterPowerRandomChangeValue;
        newGameData.MonsterStatRandomChangeValue = monsterData[index].MonsterStatRandomChangeValue;
        return newGameData;
    }

    private void SetMonsterObj()
    {
        int monsterIndex;
        for (int i = 0; i < monsterSpawnCount; i++)
        {
            monsterIndex = Random.Range(0, GameManager.Instance.gameExcelData.MonsterData.Count - 2);
            if (monsterIndex % 2 == 1)
            {
                monsterIndex--;
            }
            monsterIndex += CheckEliteMonsterRate(monsterIndex);

            //monsterList.Add(Instantiate(Resources.Load<GameObject>(GameManager.Instance.gameExcelData.MonsterData[monsterIndex].MonsterName), gameObject.transform));

            switch (monsterSpawnCount)
            {
                case 1:
                    if (monsterList[2].TryGetComponent<MonsterBase>(out MonsterBase monsterbase_1))
                    {
                        monsterbase_1.monsterData = DeepCopy(monsterIndex, Resources.Load<GameData>("GameData").MonsterData);
                        monsterbase_1.currentMonsterIndex = 2;

                        MonsterStatRandomChange(monsterbase_1);
                        AddMonsterDropItem(monsterbase_1);

                        SetMonsterSpineData(2, monsterbase_1.monsterData.UID);
                    }
                    break;
                case 2:
                    if (monsterList[1+i*2].TryGetComponent<MonsterBase>(out MonsterBase monsterbase_2))
                    {
                        monsterbase_2.monsterData = DeepCopy(monsterIndex, Resources.Load<GameData>("GameData").MonsterData); //Resources.Load<GameData>("GameData").MonsterData[monsterIndex];
                        monsterbase_2.currentMonsterIndex = 1 + i * 2;

                        MonsterStatRandomChange(monsterbase_2);
                        AddMonsterDropItem(monsterbase_2);

                        SetMonsterSpineData(1 + i * 2, monsterbase_2.monsterData.UID);
                    }
                    break;
                default:
                    if (monsterList[0 + i * 2].TryGetComponent<MonsterBase>(out MonsterBase monsterbase_3))
                    {
                        monsterbase_3.monsterData = DeepCopy(monsterIndex, Resources.Load<GameData>("GameData").MonsterData);
                        monsterbase_3.currentMonsterIndex = 0 + i * 2;

                        MonsterStatRandomChange(monsterbase_3);
                        AddMonsterDropItem(monsterbase_3);

                        SetMonsterSpineData(0 + i * 2, monsterbase_3.monsterData.UID);
                    }
                    break;
            }
            /*if(monsterList[i].TryGetComponent<MonsterBase>(out MonsterBase monsterbase))
            {
                monsterbase.monsterData = Resources.Load<GameData>("GameData").MonsterData[monsterIndex];
                monsterbase.monsterIndex = i;
            }*/
        }
    }
    
    public void AddMonsterDropItem(MonsterBase monsterbase)
    {
        if (RandomManager.CheckWeight(100)) //GameManager.Instance.randomManager.CheckGetPotion(100)
        {
            getItems.Add(monsterbase.monsterData.MonsterDropItem);
        }
        totalEXP += monsterbase.monsterData.MonsterDropEXP;
        totalGold += monsterbase.monsterData.MonsterDropGold;
    }
    public void MonsterStatRandomChange(MonsterBase monsterbase)
    {
        int temp;

        Debug.Log("몬스터 HP 변경 전: " + monsterbase.monsterData.MonsterHP);
        monsterbase.monsterData.MonsterHP += RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterStaminaRandomeChangeValue); //GameManager.Instance.randomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterStaminaRandomeChangeValue)
        Debug.Log("몬스터 HP 변경 후: " + monsterbase.monsterData.MonsterHP);
        monsterbase.monsterData.MonsterMP += RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterStaminaRandomeChangeValue);

        monsterbase.monsterData.MonsterAD = (temp = monsterbase.monsterData.MonsterAD + RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterPowerRandomChangeValue)) <= 0 ? 0 : temp;
        monsterbase.monsterData.MonsterAP = (temp = monsterbase.monsterData.MonsterAP + RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterPowerRandomChangeValue)) <= 0 ? 0 : temp;
        monsterbase.monsterData.MonsterDefensive_AD = (temp = monsterbase.monsterData.MonsterDefensive_AD + RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterPowerRandomChangeValue)) <= 0 ? 0 : temp;
        monsterbase.monsterData.MonsterDefensive_AP = (temp = monsterbase.monsterData.MonsterDefensive_AD + RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterPowerRandomChangeValue)) <= 0 ? 0 : temp;

        monsterbase.monsterData.MonsterStrength = (temp = monsterbase.monsterData.MonsterStrength + RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterPowerRandomChangeValue)) <= 0 ? 0 : temp;
        monsterbase.monsterData.MonsterAgility = (temp = monsterbase.monsterData.MonsterAgility + RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterPowerRandomChangeValue)) <= 0 ? 0 : temp;
        monsterbase.monsterData.MonsterMagicPower = (temp = monsterbase.monsterData.MonsterMagicPower + RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterPowerRandomChangeValue)) <= 0 ? 0 : temp;
        monsterbase.monsterData.MonsterLuck = (temp = monsterbase.monsterData.MonsterLuck + RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterPowerRandomChangeValue)) <= 0 ? 0 : temp;


        monsterbase.monsterData.MonsterDropEXP += RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterDropEXP / (int) RandomRatio.FixedRatio);
        monsterbase.monsterData.MonsterDropGold += RandomManager.ApplyFixedRatioValue(monsterbase.monsterData.MonsterDropGold / (int)RandomRatio.FixedRatio);
    }

   

    private void SetMonsterSpineData(int listIndex, int monsterUID)
    {
        int monsterIndex = GameManager.Instance.gameExcelData.MonsterScale.FindIndex(x => x.UID == monsterUID);
        Debug.Log("몬스터 인덱스: " + monsterIndex);
        Debug.Log("리스트 인덱스: " + listIndex);
        if (monsterList[listIndex].TryGetComponent<SkeletonAnimation>(out SkeletonAnimation anim))
        {
            anim.skeletonDataAsset = Resources.Load<SkeletonDataAsset>(GameManager.Instance.gameExcelData.MonsterScale[monsterIndex].DataPath);
            anim.initialSkinName = GameManager.Instance.gameExcelData.MonsterScale[monsterIndex].InitialSkin;

            Debug.Log("스켈레톤 데이터 에셋: " + anim.skeletonDataAsset);
            Debug.Log("초기스킨이름: " + anim.initialSkinName);
            //anim.Initialize(true);
            anim.gameObject.LeanScaleX(GameManager.Instance.gameExcelData.MonsterScale[monsterIndex].MonsterScaleX, 0f);
            anim.gameObject.LeanScaleY(GameManager.Instance.gameExcelData.MonsterScale[monsterIndex].MonsterScaleY, 0f);


            anim.loop = true;
            anim.AnimationName = MonsterAnimState.Idle.ToString();
            //skeletonAnimation.AnimationState.Apply(skeletonAnimation.Skeleton);

            //anim.AnimationState.Apply(anim.skeleton);
            anim.Initialize(true);
            //SpineEditorUtilities.ReloadSkeletonDataAssetAndComponent(anim);


        }
    }
    public void MonsterChange()
    {
        int ix = 0;
        if (monsterList[0].TryGetComponent<SkeletonAnimation>(out SkeletonAnimation anim))
        {
            //anim.skeletonDataAsset = Resources.Load<GameObject>("EliteSlime").GetComponent<SkeletonAnimation>().skeletonDataAsset;
            //anim.skeletonDataAsset = Resources.Load<GameObject>(GameManager.Instance.gameExcelData.MonsterScale[ix].MonsterName).GetComponent<SkeletonAnimation>().skeletonDataAsset;
            anim.skeletonDataAsset = Resources.Load<SkeletonDataAsset>(GameManager.Instance.gameExcelData.MonsterScale[ix].DataPath);
            anim.initialSkinName = GameManager.Instance.gameExcelData.MonsterScale[ix].InitialSkin;

            //SpineEditorUtilities.ReloadSkeletonDataAssetAndComponent(anim);

            anim.gameObject.LeanScaleX(GameManager.Instance.gameExcelData.MonsterScale[ix].MonsterScaleX, 0f);
            anim.gameObject.LeanScaleY(GameManager.Instance.gameExcelData.MonsterScale[ix].MonsterScaleY, 0f);
            

            anim.loop = true;
            anim.AnimationName = MonsterAnimState.Idle.ToString();
            ix++;
        }
    }

    float time;
    private bool isFind = false;
    //[SerializeField] Canvas canvas;
    GameObject[] hpbarList;
    public bool playerAttacked = false;
    private void Update()
    {
        GameManager.Instance.AddTime();
        //time += Time.deltaTime;

        /*if (isClicked)
        {
            Mathf.Lerp(startPos.x, EndPos.x, 1);
            playerAnim.AnimationName = "Idle";
        }*/
        if (playerAttacked)
        {
            
        }
        if(time >= 2f)
        {
            time -= 2f;

            /*Debug.Log(monsterList[0].name);
            if (monsterList[0].TryGetComponent<MonsterBase>(out MonsterBase monsterbase))
            {
                
                monsterbase.printData();
            }

            if (monsterList[1].TryGetComponent<MonsterBase>(out MonsterBase monsterbase2))
            {
                monsterbase2.printData();
            }*/

            /*if (!isFind)
            {
                if (GameObject.FindGameObjectWithTag("UI").TryGetComponent<StateBarController>(out StateBarController state))
                {
                    state.HPbarTextChange(monsterList[0].GetComponent<MonsterBase>().monsterData.MonsterHP);
                }
                isFind = !isFind;
            }*/
            //StartCoroutine("MonsterClick");
            if (isFind)
            {
                //hpbarList[0] = canvas.transform.GetChild(3);
                //hpbarList[0] = canvas.transform.GetChild(4);
                //if (canvas.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
                //{
                //    text.text = monsterList[0].GetComponent<MonsterBase>().monsterData.MonsterHP.ToString();
                //}
                //if (canvas.transform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text2))
                //{
                //    text2.text = monsterList[1].GetComponent<MonsterBase>().monsterData.MonsterHP.ToString();
                //}
              
                //isFind = !isFind;
            }

            //if(GameObject.find)
        }
    }
    private int CheckEliteMonsterRate(int index)
    {
        Debug.Log("랜덤매니저 체크: " + RandomManager.CreateRandomByValue(100));
        //Debug.Log("랜덤매니저 체크: " + RandomManager.CreateRandomByValue(100));
        if (GameManager.Instance.gameExcelData.MonsterData[index].EliteMonsterRate < RandomManager.CreateRandomByValue(100))
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public GameObject player;
    public void PlayerSpawn()
    {
        player = Instantiate(Resources.Load<GameObject>("Player"), new Vector3(-5f, -3.35f, 0f), Quaternion.identity);
    }

    

   /* List<GameObject> objectList_selectable = new List<GameObject>();
    List<GameObject> objectList_select = new List<GameObject>();

    [SerializeField] GameObject selectableMonsterImg;
    [SerializeField] GameObject selectMonsterImg;*/

    public bool isSelecting = true;
    public void ShowSelectableMonster(int index)
    {
        if (!isPlayerAttack)
        {
            if (isSelecting)
            {
                isSelecting = !isSelecting;
                for (int i = 0; i < monsterList.Count; i++)
                {
                    /*GameObject createImg = Instantiate(selectableMonsterImg, monsterList[i].transform.position, Quaternion.Euler(new Vector3(60f, 0f, 0f)), transform);
                    GameObject createImg2 = Instantiate(selectMonsterImg, monsterList[i].transform.position + new Vector3(0f, 1.25f, 0f), Quaternion.identity, transform);
                    objectList_selectable.Add(createImg);  
                    objectList_select.Add(createImg2);    */

                    monsterList[i].transform.GetChild(0).gameObject.SetActive(true);
                    monsterList[i].transform.GetChild(1).gameObject.SetActive(true);

                    /*if (monsterList[i].transform.GetChild(1).gameObject.TryGetComponent<SelectEvent>(out SelectEvent selectevent))
                    {

                    }*/
                }
            }
            else
            {
                isSelecting = true;
                for (int i = 0; i < monsterList.Count; i++)
                {

                    monsterList[i].transform.GetChild(0).gameObject.SetActive(false);
                    monsterList[i].transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
    }

    /*public bool isSelected = false;
    private IEnumerator MonsterClick()
    {
        yield return null;
        while (true)
        {
            if (!isSelected)
            {
                Debug.Log("monsterClick 실행");
                StopCoroutine("MonsterClick");
            }
            yield return null;
        }
    }*/

    /*public int GetMonsterHP()
    {
        MonsterBase monsterData = GetComponentInParent<MonsterBase>();
        return monsterData.currentHP;
    }*/

    public bool CheckMonsterDie(int currentHP)
    {
        if(currentHP <= 0)
        {
            return true;
        } else
        {
            return false;
        }
    }


    public bool isClicked = false;
    public void MonsterDamaged(GameObject temp)
    {
      
        this.isSelecting = !this.isSelecting;
        for(int i = 0; i < monsterList.Count; i++)
        {
            monsterList[i].transform.GetChild(0).gameObject.SetActive(false);
            monsterList[i].transform.GetChild(1).gameObject.SetActive(false);
        }
        

        MonsterBase monsterData = temp.GetComponentInParent<MonsterBase>();

       
        
        if(player.transform.GetChild(0).TryGetComponent<SkeletonAnimation>(out SkeletonAnimation playerAnim))
        {
            StartCoroutine(PlayerAttack(playerAnim, monsterData, this, player.transform.position, temp.transform.parent.position));
        }

        
    }
    public int SkillAttack()
    {
        return 10;
    }

    public void SceneMove()
    {
        SceneManager.LoadScene("VillageScene");
    }

    public void StateBarChange(int index)
    {
        if(GameObject.FindGameObjectWithTag("UI").TryGetComponent<StateBarController>(out StateBarController state))
        {
            state.HPbarTextChange(index, GameManager.playerData.PlayerAD + GameManager.playerData.PlayerStrength);
        }
    }

    private IEnumerator PlayerAttack(SkeletonAnimation  playerAnim, MonsterBase monsterData, BattleController controller,Vector3 startPos, Vector3 EndPos)
    {
        float time = 0f;
        float currentTime = 0f;
        float endTime = 0.3f;

        Vector3 pos = startPos;

        playerAnim.AnimationName = PlayerState.Run.ToString();
        while (true)
        {
            currentTime += Time.deltaTime;
            
            playerAnim.transform.parent.position = Vector3.Lerp(startPos, EndPos - new Vector3(1.2f, 0f, 0f), currentTime / endTime);

            if ((currentTime / endTime) >= 1)
            {
                currentTime = 0f;
                break;
            }
            yield return new WaitForSeconds(1 / 1000f);
        }
        yield return null;
        
        playerAnim.loop = false;

        if(RandomManager.CreateRandomByValue(100) % 2 == (int)PlayerState.Attack1)
        { 
            playerAnim.AnimationName = PlayerState.Attack1.ToString();
        } else
        {
            playerAnim.AnimationName = PlayerState.Attack2.ToString();
        }

        yield return new WaitForSeconds(0.9f);
        
        controller.StateBarChange(monsterData.currentMonsterIndex);
        playerAnim.loop = true;
        playerAnim.AnimationName = PlayerState.Idle.ToString();
        while (true)
        {
            currentTime += Time.deltaTime;

            playerAnim.transform.parent.position = Vector3.Lerp(EndPos - new Vector3(1.2f, 0f, 0f),startPos , currentTime / endTime);
            //playerAnim.transform.parent.position = Vector3.Lerp(startPos, EndPos-new Vector3(1.2f, 0f, 0f), 1f);
            if ((currentTime / endTime) >= 1)
            {
                currentTime = 0f;
                break;
            }
            yield return new WaitForSeconds(1 / 1000f);
        }
        //isPlayerAttack = !isPlayerAttack;
        //Debug.Log(isPlayerAttack);
        StopCoroutine("PlayerAttack");
    }

    public void StartMonsterAttack()
    {
        StartCoroutine(MonsterAttack());
    }
    public IEnumerator MonsterAttack()
    {
        yield return null;

        float time = 0f;
        float currentTime = 0f;
        float endTime = 0.3f;

        Vector3 startPos;

        int monsterCount = monsterSpawnCount;

        for (int i = 0; i < monsterList.Count; i++)
        {
            if (isPlayerDie)
            {
                break;
            }
            if (monsterList[i].activeSelf)
            {
                startPos = monsterList[i].transform.position;

                if(monsterList[i].TryGetComponent<SkeletonAnimation>(out SkeletonAnimation monsterAnim))
                {
                    monsterAnim.AnimationName = MonsterState.Walk.ToString();

                    while (true)
                    {
                        currentTime += Time.deltaTime;

                        monsterAnim.transform.position = Vector3.Lerp(startPos, player.gameObject.transform.position + new Vector3(1.2f, 0f, 0f), currentTime / endTime);

                        if ((currentTime / endTime) >= 1)
                        {
                            currentTime = 0f;
                            break;
                        }
                        yield return new WaitForSeconds(1 / 1000f);
                    }

                    monsterAnim.loop = false;

                    monsterAnim.AnimationName = MonsterState.Attack.ToString();

                    yield return new WaitForSeconds(0.9f);

                    if (GameObject.FindGameObjectWithTag("UI").TryGetComponent<StateBarController>(out StateBarController state))
                    {
                        if(monsterList[i].TryGetComponent<MonsterBase>(out MonsterBase monsterbase))
                        {
                            state.PlayerHPBarTextChange(monsterbase.monsterData.MonsterAD + monsterbase.monsterData.MonsterStrength, player.GetComponent<PlayerBase>());
                        }
                        
                    }

                    monsterAnim.loop = true;
                    monsterAnim.AnimationName = MonsterState.Idle.ToString();

                    while (true)
                    {
                        currentTime += Time.deltaTime;

                        monsterAnim.transform.position = Vector3.Lerp(monsterAnim.gameObject.transform.position, startPos, currentTime / endTime);
                        //playerAnim.transform.parent.position = Vector3.Lerp(startPos, EndPos-new Vector3(1.2f, 0f, 0f), 1f);
                        if ((currentTime / endTime) >= 1)
                        {
                            currentTime = 0f;
                            break;
                        }
                        yield return new WaitForSeconds(1 / 1000f);
                    }

                    Debug.Log((i == (int)MonsterLastIndex.LastIndexFromThreeSpawned) || (i == (int)MonsterLastIndex.LastIndexFromThreeSpawned && !monsterList[(int)MonsterLastIndex.LastIndexFromThreeSpawned].activeSelf));
                    switch (monsterSpawnCount)
                    {
                        case 1:
                            if (i == (int)MonsterLastIndex.LastIndexFromOneSpawned)
                            {
                                isPlayerAttack = !isPlayerAttack;
                                isSelecting = true;
                            }
                            break;
                        case 2:
                            if ((i == (int)MonsterLastIndex.LastIndexFromTwoSpawned))
                            {
                                isPlayerAttack = !isPlayerAttack;
                                isSelecting = true;
                            } else if((monsterActiveCount == 1 && i == 1) && !monsterList[3].activeSelf)
                            {
                                isPlayerAttack = !isPlayerAttack;
                                isSelecting = true;
                            }
                            break;
                        default:
                            if ((i == (int)MonsterLastIndex.LastIndexFromThreeSpawned))
                            {
                                isPlayerAttack = !isPlayerAttack;
                                isSelecting = true;
                                
                            } else if((monsterActiveCount == 2 && i == 2) && !monsterList[4].activeSelf)
                            {
                                isPlayerAttack = !isPlayerAttack;
                                isSelecting = true;
                            } else if((monsterActiveCount == 1 && i == 0) && !monsterList[2].activeSelf)
                            {
                                isPlayerAttack = !isPlayerAttack;
                                isSelecting = true;
                            }
                            break;
                    }
                }
            }
        }
        StopCoroutine(MonsterAttack());
    }

    public void BackToLobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void BackToVillageScene()
    {
        GameManager.playerData.CurrentGold += totalGold;
        GameManager.playerData.CurrentEXP += totalEXP;
        if (player.TryGetComponent<PlayerBase>(out PlayerBase playerData))
        {
            GameManager.playerData.PlayerCurrentHP = playerData.currentHP;
            //GameManager.playerData.PlayerCurrentMP = playerData.currentMP;
        }
        GameManager.PlayerDataSave();
        SceneManager.LoadScene("VillageScene");
    }

    public void BackToBattleMoveScene()
    {
        GameManager.playerData.CurrentGold += totalGold;
        GameManager.playerData.CurrentEXP += totalEXP;
        if (player.TryGetComponent<PlayerBase>(out PlayerBase playerData))
        {
            GameManager.playerData.PlayerCurrentHP = playerData.currentHP;
            //GameManager.playerData.PlayerCurrentMP = playerData.currentMP;
        }
        GameManager.PlayerDataSave();
        SceneManager.LoadScene("BattleMoveScene");
    }
    /*public bool ReinforceStoneWeightInit()
    {
        
    }*/

    public int GetReinforceStone()
    {
        if(RandomManager.CheckWeight(GameManager.playerData.PlayerLuck + (int) RandomRatio.Half))
        {
            return Mathf.RoundToInt(time / 900f * (float)GameManager.Instance.monsterKillScore / 10f * RandomManager.CreateRandomByFloatValue((1f + (float)(GameManager.playerData.PlayerLuck / 10f))));
        } else
        {
            return Mathf.RoundToInt(time / 900f * (float)GameManager.Instance.monsterKillScore / 10f * RandomManager.CreateRandomByFloatValue((0.5f + (float)(GameManager.playerData.PlayerLuck / 100f))));
        }
    }


}
