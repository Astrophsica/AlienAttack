using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] EnemyTypes;
    [SerializeField]
    GameObject[] SpawnPoints;
    [SerializeField]
    Transform StronholdTransform;
    [SerializeField]
    TextAsset map_json;
    MapWaveData data;

    EnemyData[] currentEnemies;
    
    private int wavePointer = 0, enemyPointer;
    private float timeIntoWave = 0, waveDelay = 0, timeFromLastEnemy = 0;
    public int CurrentWave { get { return wavePointer;} }
    private bool waveEnded = true;

    void Start()
    {
        data = JsonUtility.FromJson<MapWaveData>(map_json.text);
    }

    void Update()
    {
        if (wavePointer > data.waves.Length) 
        {
            Debug.Log("End of waves");
            return; 
        }
        timeIntoWave += Time.deltaTime;
        timeFromLastEnemy += Time.deltaTime;
        if (timeIntoWave < waveDelay) { return; }
        if (waveEnded) { StartNewWave(); }
        if (EnemyManager.enemies.Count == 0 && enemyPointer >= currentEnemies.Length)
        {
            enemyPointer = 0;
            waveEnded = true;
        }
        if (!waveEnded)
        {
            if (enemyPointer >= currentEnemies.Length ||
                timeFromLastEnemy < currentEnemies[enemyPointer].delay ||
                currentEnemies[enemyPointer].Spawned) { return; }
            Vector3 randomSpawnPoint = GetRandomSpawnPoint();

            var enemy = EnemyManager.SpawnNewEnemy(currentEnemies[enemyPointer].type, randomSpawnPoint);
            enemy.GetComponent<Enemy>().SetTarget(StronholdTransform);

            currentEnemies[enemyPointer].Spawned = true;
            enemyPointer++;
            timeFromLastEnemy = 0;
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        return SpawnPoints[Mathf.FloorToInt(Random.value * SpawnPoints.Length)].transform.position;
    }

    void StartNewWave()
    {
        waveEnded = false;
        var newWave = data.waves[wavePointer++];
        currentEnemies = newWave.enemies;
        waveDelay = newWave.delay;
        timeIntoWave = 0;
        timeFromLastEnemy = 0;
    }
}
