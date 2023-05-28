using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    

    private float startPos = -6.6f;
    private float endPos = 6.6f;
    private float increaseRate = 0f;

    private bool clicked = false;
    private Vector3 startPoint;
    public void BtnClicked()
    {
        clicked = true;
        StartCoroutine(Move());
    }

    public void BtnReset()
    {
        transform.position = new Vector3(-6f, -2.2f, 0);
    }

    IEnumerator Move()
    {
        yield return null;
        startPoint = transform.position;
        anim.SetBool("isWalk", true);

        while (startPoint.x < endPos)
        {
            startPoint.x += 15f * Time.deltaTime;

            transform.position = startPoint;
            yield return null;
        }
        anim.SetBool("isWalk", false);
    }

    IEnumerator AttackStart()
    {
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttack", false);
    }
}
