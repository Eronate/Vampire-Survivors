using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }
    public List<Drops> drops;

    void OnDestroy()
    {
        if(GameManager.instance.currentState != GameManager.GameState.GameOver)
        {
            float hp = 0;
            EnemyStats enemyStats = GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                hp = enemyStats.getHealth();
            }
            BreakableProps breakableProp = GetComponent<BreakableProps>();
            if (breakableProp != null)
            {
                hp = breakableProp.getHealth();
            }
            if (hp <= 0)
            {
                float randomNumber = UnityEngine.Random.Range(0f, 100f);
                List<Drops> possibleDrops = new List<Drops>();
                foreach (Drops rate in drops)
                {
                    if (randomNumber <= rate.dropRate)
                    {
                        possibleDrops.Add(rate);
                    }
                }
                if (possibleDrops.Count > 0)
                {
                    Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
                    Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
