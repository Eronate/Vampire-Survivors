using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    // Start is called before the first frame update
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float  currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float despawnDistance = 20f;
    Transform player;
    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;

    }

    private void Update()
    {
        if(Vector2.Distance(transform.position,player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public virtual void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            Kill();
        }

    }
    public virtual void Kill()
    {
        Destroy(gameObject);
    }
    protected virtual void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>(); 
        es.OnEnemyKilled();
    }

    public float getHealth()
    {
        return currentHealth;
    }

    void ReturnEnemy()
    {
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
