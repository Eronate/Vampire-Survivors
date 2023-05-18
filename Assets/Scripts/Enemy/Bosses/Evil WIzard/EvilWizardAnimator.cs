using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardAnimator : MonoBehaviour
{
    Animator animator;
    EvilWizardMovement enemyMovement;
    SpriteRenderer spriteRenderer;
    int hitTriggerHash;
    int attack1TriggerHash;
    int attack2TriggerHash;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EvilWizardMovement>();
        hitTriggerHash = Animator.StringToHash("Hit");
        attack1TriggerHash = Animator.StringToHash("Attack1");
        attack2TriggerHash = Animator.StringToHash("Attack2");
    }
    void Update()
    {
        if (enemyMovement.moveDir.x != 0 || enemyMovement.moveDir.y != 0)
        {
            animator.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }
    void SpriteDirectionChecker()
    {
        if (enemyMovement.moveDir.x < 0)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }
    public void TriggerAttack1()
    {
        animator.SetTrigger(attack1TriggerHash);
    }
    public void TriggerAttack2()
    {
        animator.SetTrigger(attack2TriggerHash);
    }
    public void TriggerDeath()
    {
        animator.SetBool("Die", true);
    }
    public void TriggerHit()
    {
        animator.SetTrigger(hitTriggerHash);
    }
}
