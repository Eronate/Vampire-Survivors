using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadExecutionerAnimator : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    int hitTriggerHash;
    int attackTriggerHash;
    int healTriggerHash;
    int spellTriggerHash;
    void Start()
    {
        animator = GetComponent <Animator>();
        spriteRenderer= GetComponent <SpriteRenderer>();
        hitTriggerHash = Animator.StringToHash("Hit");
        attackTriggerHash = Animator.StringToHash("Attack"); // range attack
        spellTriggerHash = Animator.StringToHash("Spell");
        healTriggerHash = Animator.StringToHash("Heal");
    }
    void Update()
    {
        Vector2 direction = transform.position - FindObjectOfType<PlayerMovement>().transform.position;
        if(direction.x < 0)
        {
            spriteRenderer.flipX= false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    public void TriggerAttack()
    {
        animator.SetTrigger(attackTriggerHash);    
    }
    public void TriggerHit()
    {
        animator.SetTrigger(hitTriggerHash);
    }
    public void TriggerDeath()
    {
        animator.SetBool("Die", true);
    }
    public void TriggerSpell()
    {
        animator.SetTrigger(spellTriggerHash);
    }
    public void TriggerHeal()
    {
        animator.SetTrigger(healTriggerHash);
    }
}
