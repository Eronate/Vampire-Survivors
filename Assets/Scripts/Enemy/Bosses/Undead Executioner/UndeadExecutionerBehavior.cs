using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadExecutionerBehavior : MonoBehaviour
{
    float timeSinceLastAction = 0f;
    int spellFrequency;
    int healFrequency;
    float minTimeSinceLastAction;
    float maxTimeSinceLastAction;
    UndeadExecutionerStats stats;
    void Start()
    {
        stats = GetComponent<UndeadExecutionerStats>();
        UndeadExecutionerScriptableObject enemyData = (UndeadExecutionerScriptableObject)stats.enemyData;
        spellFrequency = enemyData.SpellFrequency;
        healFrequency = enemyData.HealFrequency;
        minTimeSinceLastAction = enemyData.MinTimeSinceLastAction;
        maxTimeSinceLastAction = enemyData.MaxTimeSinceLastAction;
    }
    void Update()
    {
        if(stats.meleeRange.isDetected == true)
        {
            stats.animator.TriggerAttack();
        }
        else if(stats.spellRange.isDetected == true)
        {
            timeSinceLastAction -= Time.deltaTime;
            if (timeSinceLastAction <= 0f)
            {
                timeSinceLastAction = Random.Range(minTimeSinceLastAction, maxTimeSinceLastAction);
                ChooseNewAction();
            }
        }
    }
    void ChooseNewAction()
    {
        int chance = Random.Range(0, spellFrequency + healFrequency + 1);
        if(chance < healFrequency)
        {
            stats.animator.TriggerHeal();
        }
        else
        {
            stats.animator.TriggerSpell();
        }
    }
}
