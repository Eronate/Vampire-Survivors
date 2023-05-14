using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardStats : EnemyStats
{
    [HideInInspector]
    public EvilWizardAnimator animator;
    [HideInInspector]
    public DetectionScript rangeBox;
    [HideInInspector]
    public float currentMultipler;
    void Start()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        animator = GetComponent<EvilWizardAnimator>();
        rangeBox = transform.Find("RangeBox").GetComponent<DetectionScript>();
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
        if (rangeBox.isDetected == true)
        {
            PlayerStats player = FindObjectOfType<PlayerStats>();
            player.TakeDamage(currentDamage * currentMultipler);
        }
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
