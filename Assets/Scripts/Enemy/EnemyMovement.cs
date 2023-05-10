using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // public EnemyScriptableObject enemyData; this is no longer needed

    EnemyStats enemy;
    Transform player;
    public Vector2 moveDir;
    
    void Start()
    {
        enemy=GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = transform.position - player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
    }
}
