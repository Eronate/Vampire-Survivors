using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EvilWizardMovement : MonoBehaviour
{
    EvilWizardStats stats;
    Transform player;
    public Vector2 moveDir;
    int baseAttackFrequency;
    float multipler;

    void Start()
    {
        stats = GetComponent<EvilWizardStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
        stats = GetComponent<EvilWizardStats>();
        EvilWizardScriptableObject enemyData = (EvilWizardScriptableObject)stats.enemyData;
        baseAttackFrequency = enemyData.BaseAttackFrequency;
        multipler = enemyData.SpecialAttackMultipler;
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.rangeBox.isDetected == true)
        {
            ChooseAttack();
        }
        else if(stats.currentHealth > 0)
        {
            moveDir = transform.position - player.position;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, stats.currentMoveSpeed * Time.deltaTime);
        }
    }
    void ChooseAttack()
    {
        int chance = Random.Range(0, 101);
        if(chance < baseAttackFrequency)
        {
            stats.currentMultipler = 1f;
            stats.animator.TriggerAttack1();
        }
        else
        {
            stats.currentMultipler = multipler;
            stats.animator.TriggerAttack2();
        }
    }
}
