using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEvent : MonoBehaviour
{
    //[SerializeField] GameObject controller;

    private void OnMouseDown()
    {
        /*if (controller.gameObject.TryGetComponent<BattleController>(out BattleController con))
        {
            con.MonsterDamaged(this.gameObject);
        }*/

        BattleController.Instance.isPlayerAttack = !BattleController.Instance.isPlayerAttack;
        BattleController.Instance.MonsterDamaged(this.gameObject);
        
    }
}
