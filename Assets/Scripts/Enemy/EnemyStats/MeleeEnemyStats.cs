using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyStats : EnemyStats
{
    // Start is called before the first frame update
    Animator animator;
    int hitTriggerHash;
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
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isEnemyInContact = true;
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(currentDamage);
            animator.SetTrigger(attackTriggerHash);
            collider = collision;
        }
    }
    private void Death()
    {
        Destroy(gameObject);
    }
    private void DoDamage()
    {
        if(isEnemyInContact == true)
        {
            PlayerStats player = collider.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
}
