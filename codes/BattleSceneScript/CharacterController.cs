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

        //LeanTween.moveLocalX(gameObject, endPos, 3f);
        while (startPoint.x < endPos)
        {
            startPoint.x += 15f * Time.deltaTime;

            //startPoint.x = Mathf.Lerp(startPos, endPos, increaseRate);
            transform.position = startPoint;
            yield return null;
        }
        anim.SetBool("isWalk", false);
    }
    private void Update()
    {
        /*if (clicked)
        {
            startPoint = transform.position;
            Debug.Log("asd");
            if (startPoint.x <= endPos)
            {
                
                anim.SetBool("isWalk", true);
                //startPoint.x += 3f * Mathf.Pow(1+Time.deltaTime, 2);
                //startPoint.x +
                transform.position = startPoint;
            }
            else
            {
                anim.SetBool("isWalk", false);
                clicked = false;
                StartCoroutine(AttackStart());
            }
        }*/
    }
    IEnumerator AttackStart()
    {
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttack", false);
    }
}
