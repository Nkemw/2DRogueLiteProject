using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StateBarController : Singleton<StateBarController>
{
    [SerializeField] GameObject HPBar;

    List<GameObject> objectList = new List<GameObject>();
    List<Transform> objectTransform = new List<Transform>();

    Camera cam;
    // Start is called before the first frame update

    public void HPbarTextChange(int value)
    {
        /*if(HPBar.GetComponentInChildren<GameObject>().GetComponentInChildren<GameObject>().GetComponentInChildren<GameObject>().TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI hpText))
        {
            hpText.text = value.ToString();
        }*/
        if(HPBar.transform.GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
        {
            text.text = value.ToString();
        }
    }

    public void MPbarTextChange(int value)
    {
        if(HPBar.transform.GetChild(1).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
        {
            text.text = value.ToString();
        }
    }
    public void HPbarTextChange(int index, int damage)
    {
        //Debug.Log("������: " + damage);
        //Debug.Log(GameObject.FindGameObjectWithTag("UI").transform.GetChild(index + 3).GetChild(0).GetChild(0));
        /*if(GameObject.FindGameObjectWithTag("UI").transform.GetChild(index+3).GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
        {
            Debug.Log("������: " + damage);
            text.text = (int.Parse(text.text) - damage).ToString();
        }*/

        if(GameObject.Find("BattleManager").transform.GetChild(index).TryGetComponent<MonsterBase>(out MonsterBase monsterBase))
        {
            monsterBase.TakeDamage(damage);
        }

        if (GameObject.FindGameObjectWithTag("UI").transform.GetChild(index + 5).GetChild(0).TryGetComponent<Image>(out Image hpImg))
        {
            if (hpImg.gameObject.transform.GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
            {
                Debug.Log("������: " + damage);
                hpImg.fillAmount = (float.Parse(text.text) - damage) / (float.Parse(text.text) / hpImg.fillAmount);
                text.text = (float.Parse(text.text) - damage).ToString();
                                                          
            }
        }
    }

    GameObject playerHPbar;
    void Start()
    {
        cam = Camera.main;

        GameObject[] EnemyList = GameObject.FindGameObjectsWithTag("Enemy");    //��ȯ�� ����
        for (int i = 0; i < EnemyList.Length; i++)
        {
            objectTransform.Add(EnemyList[i].transform);                        //��ȯ�� ���� ��ġ�� ����Ʈ�� ����

            if (EnemyList[i].TryGetComponent<MonsterBase>(out MonsterBase monster))
            {
                HPbarTextChange(monster.monsterData.MonsterHP);
                MPbarTextChange(monster.monsterData.MonsterMP);
            }

            GameObject monsterHPbar = Instantiate(HPBar, EnemyList[i].transform.position, Quaternion.identity, transform);   //HP�ٸ� ���� ��ġ�� ����
            objectList.Add(monsterHPbar);    //������ HP�ٸ� ����Ʈ�� ����
            objectList[i].transform.position = cam.WorldToScreenPoint(objectTransform[i].position + new Vector3(0f, -0.6f, 0f));

            
        }

        playerHPbar = Instantiate(HPBar, BattleController.Instance.player.transform.position, Quaternion.identity, transform);
        playerHPbar.transform.position = cam.WorldToScreenPoint(playerHPbar.transform.position + new Vector3(-0.15f, 3f, 0f));

        if(BattleController.Instance.player.TryGetComponent<PlayerBase>(out PlayerBase playerbase))
        {
            //PlayerHPBarTextInit(playerbase.playerData.PlayerHP);
            //PlayerMPBarTextInit(playerbase.playerData.PlayerMP);

            PlayerHPBarTextInit(GameManager.playerData.PlayerCurrentHP);
            PlayerMPBarTextInit(GameManager.playerData.PlayerCurrentMP);
            PlayerHPBarImgInit();
            PlayerMPBarImgInit();
        }

        BattleController.Instance.SetMonsterActiveFalse();
        BattleController.Instance.SetMonsterActiveTrue();

        SetMonsterStateActiveFalse();

        SetMonsterStateActiveTrue();

        /*switch (objectList.Count)
        {
            case 1:
                objectList[0].transform.position += new Vector3(0f, -0.5f, 0f); //������ HP�ٴ� ������ ��ġ�̹Ƿ� ������ �Ʒ��� ��ġ�ϵ��� ��ġ�� �ٽ� ���
                break;
            case 2:
                for(int i = 0; i < 2; i++)
                {
                    objectList[i].transform.position += new Vector3(0f, -0.5f, 0f); //new Vector3(410f + 280 * i, -430f);
                }
                break;
            default:
                for (int i = 0; i < 3; i++)
                {
                    objectList[i].transform.position += new Vector3(0f, -0.5f, 0f);//new Vector3(270f + 280 * i, -430f);
                }
                break;

        }*/
    }
    
    public void SetMonsterStateActiveFalse()
    {
        for(int i = 0; i < objectList.Count; i++)
        {
            objectList[i].SetActive(false);
        }
    }

    public void SetMonsterActiveFalse(int index)
    {
        objectList[index].SetActive(false);
    }

    public void SetMonsterStateActiveTrue()
    {
        switch (BattleController.Instance.monsterSpawnCount)
        {
            case 1:
                objectList[2].SetActive(true);
                break;
            case 2:
                objectList[1].SetActive(true);
                objectList[3].SetActive(true);
                break;
            default:
                objectList[0].SetActive(true);
                objectList[2].SetActive(true);
                objectList[4].SetActive(true);
                break;
        }
    }

    public void PlayerHPBarTextChange(int damage, PlayerBase playerBase)
    {
       
        

        if (GameObject.FindGameObjectWithTag("UI").transform.GetChild(10).GetChild(0).TryGetComponent<Image>(out Image hpImg))
        {
            if (playerHPbar.transform.GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
            {
                hpImg.fillAmount = (float.Parse(text.text) - damage) / (float.Parse(text.text) / hpImg.fillAmount);
                text.text = (float.Parse(text.text) - damage).ToString();
                //text.text = damage.ToString();


            }
        }
        playerBase.TakeDamage(damage);
    }

    public void PlayerHPBarTextInit(int value)
    {
        if (playerHPbar.transform.GetChild(0).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
        {
            text.text = value.ToString();
        }
    }
    public void PlayerMPBarTextInit(int value)
    {
        if (playerHPbar.transform.GetChild(1).GetChild(0).GetChild(0).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI text))
        {
            text.text = value.ToString();
        }
    }

    public void PlayerHPBarImgInit()
    {
        if (GameObject.FindGameObjectWithTag("UI").transform.GetChild(10).GetChild(0).TryGetComponent<Image>(out Image hpImg))
        {
            
            hpImg.fillAmount = (float) GameManager.playerData.PlayerCurrentHP / GameManager.playerData.PlayerHP;
        }
    }

    public void PlayerMPBarImgInit()
    {
        if (GameObject.FindGameObjectWithTag("UI").transform.GetChild(10).GetChild(1).TryGetComponent<Image>(out Image mpImg))
        {
             mpImg.fillAmount = (float) GameManager.playerData.PlayerCurrentMP / GameManager.playerData.PlayerMP;
            
        }
    }
}
