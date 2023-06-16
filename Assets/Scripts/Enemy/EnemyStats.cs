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
    public bool isEnemyInContact = false;
    Transform player;
    protected Collider2D collider;
    protected virtual void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;

    }

    protected virtual void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
            {
                ReturnEnemy();
            }
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
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isEnemyInContact = true;
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isEnemyInContact = false;
        }
    }

    private void OnDestroy()
    {
        if(FindAnyObjectByType<EnemySpawnerUpdated>()!=null)
        {
        EnemySpawnerUpdated es = FindAnyObjectByType<EnemySpawnerUpdated>(); 
        es.OnEnemyKilled();
        }
   
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
