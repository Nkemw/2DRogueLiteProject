using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum PlayerState{
    Attack1,
    Attack2,
    Death,
    Idle,
    Run,
    Buff,
    Defence,
    Walk
}
public class PlayerBase : MonoBehaviour, ICharBase
{
    public TableEntity_PlayerData playerData;

    public int currentHP;

    private void Start()
    {
        playerData = GameManager.Instance.gameExcelData.PlayerData[0];
        currentHP = playerData.PlayerHP;
    }

    public void MoveTo()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator OnDie()
    {
        yield return null;
        if (gameObject.transform.GetChild(0).TryGetComponent<SkeletonAnimation>(out SkeletonAnimation playerAnim))
        {
            playerAnim.loop = false;
            playerAnim.AnimationName = "Death";
            yield return new WaitForSeconds(playerAnim.skeleton.Data.FindAnimation(playerAnim.AnimationName).Duration + 0.2f);
        }
        BattleController.Instance.gameOverImg.gameObject.SetActive(true);
        GameManager.PlayerDataDelete();
        StopCoroutine(OnDie());
    }

    public int SkillAttack()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        GameManager.playerData.PlayerCurrentHP -= damage;
        
        if(GameManager.playerData.PlayerCurrentHP <= 0)
        {
            StartCoroutine(OnDie());
            BattleController.isPlayerDie = true;
        }
    }
}
