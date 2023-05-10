using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStats : EnemyStats
{
    // Start is called before the first frame update
    Animator animator;
    int hitTriggerHash;
    int dieTriggerHash;
    int attackTriggerHash;
    private void Start()
    {
        animator= GetComponent<Animator>();
        hitTriggerHash = Animator.StringToHash("Hit");
        attackTriggerHash = Animator.StringToHash("Attack");
    }
    public override void TakeDamage(float dmg)
    {
        animator.SetTrigger(hitTriggerHash);
        base.TakeDamage(dmg);
    }
    public override void Kill()
    {
        animator.SetBool("Die", true);
    }
    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger(attackTriggerHash);
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
