using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject selectableMonsterImg;
    [SerializeField] GameObject selectMonsterImg;

    List<GameObject> objectList_selectable = new List<GameObject>();
    List<GameObject> objectList_select = new List<GameObject>();
    List<Transform> objectTransform = new List<Transform>();
    GameObject[] EnemyList;
 

    private void Start()
    {
        //GameObject[] EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
    }
    public void ShowSelectableMonster()
    {
        GameObject[] EnemyList = GameObject.FindGameObjectsWithTag("Enemy");    //소환된 몬스터
        for (int i = 0; i < EnemyList.Length; i++)
        {
            objectTransform.Add(EnemyList[i].transform);                        //소환된 몬스터 위치를 리스트로 저장
            GameObject createImg = Instantiate(selectableMonsterImg, EnemyList[i].transform.position, Quaternion.Euler(new Vector3(60f, 0f, 0f)), transform);   
            GameObject createImg2 = Instantiate(selectMonsterImg, EnemyList[i].transform.position + new Vector3(0f, 1.25f, 0f), Quaternion.identity, transform);   
            objectList_selectable.Add(createImg);    //생성된 HP바를 리스트로 저장
            objectList_select.Add(createImg2);    //생성된 HP바를 리스트로 저장

        }

    }
}
