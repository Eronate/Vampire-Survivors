using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadExecutionerStats : EnemyStats
{
    public float healValue;
    [HideInInspector]
    public UndeadExecutionerAnimator animator;
    [HideInInspector]
    public DetectionScript meleeRange;
    [HideInInspector]
    public DetectionScript spellRange;
    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        animator = GetComponent<UndeadExecutionerAnimator>();
        meleeRange = transform.Find("RangeBox").GetComponent<DetectionScript>();
        spellRange = transform.Find("PlayerDetector").GetComponent<DetectionScript>();
    }
    public override void TakeDamage(float dmg)
    {
        animator.TriggerHit();
        base.TakeDamage(dmg);
    }
    public override void Kill()
    {
        animator.TriggerDeath();
    }
    public void MeleeAttack()
    {
        if(meleeRange.isDetected == true)
        {
            PlayerStats player = FindObjectOfType<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
    public void SpellAttack()
    {
        if(spellRange.isDetected == true)
        {
            PlayerStats player = FindObjectOfType<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
    public void Heal()
    {
        currentHealth += healValue;
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
