using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEvent : MonoBehaviour
{
    private void OnMouseDown()
    {
        BattleController.Instance.isPlayerAttack = !BattleController.Instance.isPlayerAttack;
        BattleController.Instance.MonsterDamaged(this.gameObject);
        
    }
}
