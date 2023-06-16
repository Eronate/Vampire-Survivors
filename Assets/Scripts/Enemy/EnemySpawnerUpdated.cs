using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnemySpawner;

public class EnemySpawnerUpdated : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        //public List<EnemyGroup> enemyGroups; A list of groups of enemies to spawn in this wave
        public List<GameObject> enemies;
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

    public float[] probabilitiesOfEnemySpawn = { 0.7f, 0.12f, 0.12f, 0.06f };

    public float probabilityOfIdenticalWave = 0.3f;

    public float enemyCountCoefficient = 1.3f;

    public int maxQuota;

    float waveTimer;
   
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        waves = new List<Wave>();
        Wave firstWave = new Wave();
        firstWave.enemies = new List<GameObject>();
        firstWave.waveQuota = 7;
        for (int i = 1; i <= firstWave.waveQuota; i++)
            firstWave.enemies.Add(listOfEnemies[0]);
        firstWave.spawnInterval = 1;
        firstWave.spawnCount = 0;
        waves.Add(firstWave);
        currentWaveCount = 0;
        waveInterval = 8;
        maxQuota = 7;
    }

    // Update is called once per frame
    void Update()
    {
        waveTimer += Time.deltaTime;
        if (waveTimer >= waveInterval && waves[currentWaveCount].spawnCount >= waves[currentWaveCount].waveQuota)
        {
            BeginNextWave();
            waveTimer = 0f;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }
    void BeginNextWave()
    {
        //Wait for `waveInterval` seconds before starting the next wave.
        currentWaveCount++;
        Wave wave = new Wave();

        //The next wave will have more enemies than the last, up to 1.3x
        wave.waveQuota = Math.Max(Convert.ToInt32(waves[currentWaveCount - 1].waveQuota * enemyCountCoefficient),
            Convert.ToInt32(maxQuota * enemyCountCoefficient));

        maxQuota = Math.Max(wave.waveQuota, maxQuota);

        wave.enemies = new List<GameObject>();
        if (currentWaveCount % 5 == 0)
        {
            System.Random randomInt = new System.Random();
            int randomBoss = randomInt.Next(2);
            wave.enemies.Add(listOfEnemies[4 + randomBoss]);
            wave.waveQuota = 1;
            wave.waveName = "Boss fight";
            wave.spawnInterval = 1;
        }
        else
        {
            System.Random random = new System.Random();
            double randomNumber = random.NextDouble();

            if (randomNumber < probabilityOfIdenticalWave)
            {
                wave.spawnInterval = 0.3f;

                System.Random randomInt = new System.Random();
                int randomIdenticalEnemy = randomInt.Next(4);

                GameObject enemy = listOfEnemies[randomIdenticalEnemy];
                for (int i = 1; i <= wave.waveQuota; i++)
                    wave.enemies.Add(enemy);
            }
            else
            {
                wave.spawnInterval = 1;
                List<(float, float)> bins = new List<(float, float)>();
                float lowerBound = 0f;

                for (int i = 0; i < probabilitiesOfEnemySpawn.Length; i++)
                {
                    float upperBound = lowerBound + probabilitiesOfEnemySpawn[i];
                    bins.Add((lowerBound, upperBound));
                    lowerBound = upperBound;
                }
                while (wave.enemies.Count < wave.waveQuota)
                {
                    System.Random randomRandomEnemy = new System.Random();
                    double randomEnemy = randomRandomEnemy.NextDouble();
                    for (int i = 0; i < bins.Count; i++)
                    {
                        if (bins[i].Item1 <= randomEnemy && randomEnemy <= bins[i].Item2)
                            wave.enemies.Add(listOfEnemies[i]);
                    }
                }
            }
            
        }
        wave.spawnCount = 0;
        waves.Add(wave);
    }
    void SpawnEnemies()
    {
        //Check if the minimum number of enemies in the wave have been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //Spawn each type of enemy until the quota is filled
            foreach (var enemy in waves[currentWaveCount].enemies)
            {
                if (enemiesAlive >= maxEnemiesAllowed)
                {
                    maxEnemiesReached = true;
                    return;
                }
                //Spawn the enemy at a random position close to the player
                Instantiate(enemy, player.position + relativeSpawnPoints[UnityEngine.Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
                waves[currentWaveCount].spawnCount++;
                enemiesAlive++;
            }
        }
        //Reset the maxEnemiesReached flag if the number of enemies alive has dropped below the maximum amount
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }
    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }

}
