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
    TextAsset map_json;
    MapWaveData data;

    void Start()
    {
        data = JsonUtility.FromJson<MapWaveData>(map_json.text);
        int test = 0;
    }

    void Update()
    {
        
    }
}
