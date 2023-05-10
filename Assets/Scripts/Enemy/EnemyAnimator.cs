using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    Animator am;
    EnemyMovement em;
    SpriteRenderer sr;

    void Start()
    {
        am = GetComponent<Animator>();
        em = GetComponent<EnemyMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (em.moveDir.x != 0 || em.moveDir.y != 0)
        {
            am.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            am.SetBool("Move", false);
        }
    }
    void SpriteDirectionChecker()
    {
        if (em.moveDir.x < 0)
            sr.flipX = false;
        else
            sr.flipX = true;
    }
}
