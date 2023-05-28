using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum MonsterState{
    Attack,
    Dead,
    Idle,
    Walk
}

public enum MonsterLastIndex
{
    LastIndexFromOneSpawned = 2,
    LastIndexFromTwoSpawned = 3,
    LastIndexFromThreeSpawned = 4
}
public class MonsterBase : MonoBehaviour, ICharBase

{
    public TableEntity_MonsterData monsterData;

    public int currentHP;
    public int currentMonsterIndex;

    private GameObject temp;

    public Vector3 getPos { get { return getPos; } set { getPos = gameObject.transform.position; } }

    private void Start()
    {   
        currentHP = monsterData.MonsterHP;

        Debug.Log(monsterData.MonsterName + " " + monsterData.MonsterHP);
    }

    virtual public void printData()
    {
        Debug.Log(monsterData.MonsterName + " " + monsterData.MonsterHP);
    }

    public void TakeDamage(int damage)
    {
        
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Debug.Log("몬스터 사망");
            StartCoroutine(OnDie());

        }
        else
        {
            Debug.Log("몬스터 잔여 HP: " + currentHP);
            BattleController.Instance.StartMonsterAttack();
            
        }
        
    }

    public void MoveTo()
    {
        throw new System.NotImplementedException();
    }

    public int SkillAttack()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator OnDie()
    {
        Debug.Log("OnDie 실행");
        yield return null;
        if(gameObject.TryGetComponent<SkeletonAnimation>(out SkeletonAnimation monsterAnim)){
            monsterAnim.loop = false;
            monsterAnim.AnimationName = "Dead";
            yield return new WaitForSeconds(monsterAnim.skeleton.Data.FindAnimation(monsterAnim.AnimationName).Duration);
            BattleController.Instance.monsterActiveCount--;

            BattleController.Instance.SetMonsterActiveFalse(currentMonsterIndex);
            StateBarController.Instance.SetMonsterActiveFalse(currentMonsterIndex);
            CheckMonsterActive();
            RewardImgActive();
        }
        
        StopCoroutine(OnDie());
    }

    public void CheckMonsterActive()
    {
        if (BattleController.Instance.monsterActiveCount >= 1)
        {
            BattleController.Instance.StartMonsterAttack();
        }
    }
    public void RewardImgActive()
    {

        if(BattleController.Instance.monsterActiveCount == 0)
        {
            BattleController.Instance.playerRewardImg.gameObject.SetActive(true);
        }
        
    }
}
