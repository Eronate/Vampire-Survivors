using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //A list of groups of enemies to spawn in this wave
        public int waveQuota; //total number of enemies to spawn in a wave
        public float spawnInterval; //The interval at which to spawn enemies
        public int spawnCount; //The number of enemies already spawned in this wave
    }
    //0 = Bat
    //1 = Red Bat
    //2 = Shardsoul
    //3 = Skeleton
    //4 = EvilWizard
    //5 = UndeadExecutioner
    public List<GameObject> listOfEnemies;

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; //The number of enemies to spawn in this wave
        public int spawnCount; //The number of enemies of this type already spawned in this wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; //A list of all the waves in the game

    public int currentWaveCount; //The index of the current wave [list starts from 0]   

    [Header("Spawner Attributes")]
    float spawnTimer; //Timer used to determine when to spawn the next enemy;
    public int enemiesAlive;
    public int maxEnemiesAllowed; //The maximum number of enemies allowed on the map at once
    public bool maxEnemiesReached = false;
    public float waveInterval; //The interval between each wave

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; //A lsit to store all the relative spawn points of enemies

    Transform player;
    void Start()
    {

        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }


    // Update is called once per frame
    void Update()
    {
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());    
        }

        spawnTimer += Time.deltaTime;
        if(spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        //Wait for `waveInterval` seconds before starting the next wave.
        yield return new WaitForSeconds(waveInterval);
        
        //If there are more waves to start after the current wave, move on to the next wave
        if(currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    /// <summary>
    /// This method will stop spawning enemies if the amount of enemies on the map is maximum
    /// The method will only spawn enemies in a particular wave until it is time for the next wave's enemies to be spawned
    /// </summary>
    void SpawnEnemies()
    {
        //Check if the minimum number of enemies in the wave have been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //Spawn each type of enemy until the quota is filled
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //Check if the minimum number of enemies of this type have been spawned
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    //Spawn the enemy at a random position close to the player
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }
        //Reset the maxEnemiesReached flag if the number of enemies alive has dropped below the maximum amount
        if (enemiesAlive <maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    //Call this fgunction when an enemy is killed
    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }
}
