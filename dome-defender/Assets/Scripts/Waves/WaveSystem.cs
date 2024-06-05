using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSystem : Singleton<WaveSystem>
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    }

    [Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int waveIndex = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float WaveCountdown { get; private set; }
    private float searchCountdown = 1f;
    public SpawnState State { get; private set; } = SpawnState.COUNTING;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        WaveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (State == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (WaveCountdown <= 0f)
        {
            if (State != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[waveIndex]));
            }
        }

        WaveCountdown -= Time.deltaTime;
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave: " + wave.name);
        State = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        State = SpawnState.WAITING;
    }

    private void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawning enemy: " + enemy.name);
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, spawnPoint);
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave completed!");
        State = SpawnState.COUNTING;
        WaveCountdown = timeBetweenWaves;

        if (waveIndex + 1 > waves.Length - 1)
        {
            waveIndex = 0;
            Debug.Log("All waves completed! Looping...");
        }
        else
        {
            waveIndex++;
        }
    }

    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            return GameObject.FindWithTag("Enemy") != null;
        }
        return true;
    }
}
