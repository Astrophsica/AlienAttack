using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author Keiron
/// Humza added Shop code
/// </summary>
public class WaveManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] EnemyTypes;
    [SerializeField]
    public GameObject[] SpawnPoints;
    [SerializeField]
    Transform StronholdTransform;
    [SerializeField]
    TextAsset map_json;
    MapWaveData data;

    EnemyData[] currentEnemies;
    
    private int wavePointer = 0, enemyPointer, reward;
    private float timeIntoWave = 0, waveDelay = 0, timeFromLastEnemy = 0;
    public int CurrentWave { get { return wavePointer;} }
    private bool waveEnded = true;

    void Start()
    {
        data = JsonUtility.FromJson<MapWaveData>(map_json.text);
    }

    public void StartWave()
    {
        StartNewWave();
    }

    void Update()
    {
        if (GameManager.Instance.InBuildMode()) { return; }
        if (wavePointer > data.waves.Length) 
        {
            Debug.Log("End of waves");
            return; 
        }
        timeIntoWave += Time.deltaTime;
        timeFromLastEnemy += Time.deltaTime;
        if (timeIntoWave < waveDelay) { return; }
        if (waveEnded) {
            GameManager.Instance.SwitchState(); //Alternates between Build/Play mode
            return;
        }
        //Checks if all enemies dead and ensures that the wave is finished spawning.
        if (EnemyManager.enemies.Count == 0 && enemyPointer >= currentEnemies.Length)
        {
            enemyPointer = 0;
            waveEnded = true;
            ShopManager.AddMoney(reward);
        }
        if (!waveEnded)
        {
            //If the wave has finished spawning, don't do anything else.
            if (enemyPointer >= currentEnemies.Length ||
                timeFromLastEnemy < currentEnemies[enemyPointer].delay ||
                currentEnemies[enemyPointer].Spawned) { return; }
            
            //Picks a random spawn point from List of spawn points made in Unity
            Vector3 randomSpawnPoint = GetRandomSpawnPoint();
            //Spawns a new enemy at said spawn point
            var enemy = EnemyManager.SpawnNewEnemy(currentEnemies[enemyPointer].type, randomSpawnPoint);
            enemy.GetComponent<Enemy>().SetTarget(StronholdTransform);

            currentEnemies[enemyPointer].Spawned = true;
            enemyPointer++;
            timeFromLastEnemy = 0;
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        //Chooses a random indexed spawn point game object in the Serializable Array
        return SpawnPoints[Mathf.FloorToInt(Random.value * SpawnPoints.Length)].transform.position;
    }

    /// <summary>
    /// Resets variables for a new wave to spawn.
    /// </summary>
    void StartNewWave()
    {
        waveEnded = false;
        var newWave = data.waves[wavePointer++];
        currentEnemies = newWave.enemies;
        waveDelay = newWave.delay;
        reward = newWave.reward;
        timeIntoWave = 0;
        timeFromLastEnemy = 0;
    }
}
