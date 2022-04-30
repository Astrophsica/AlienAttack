using System;
using UnityEngine;
using UnityEngine.Serialization;

//Author Keiron - Used for JSON loading of waves/maps
[System.Serializable]
public class MapWaveData
{
    public string map_name;
    public WaveData[] waves;
}

[System.Serializable]
public class WaveData
{
    public float delay;
    public int reward;
    public EnemyData[] enemies;
}

[System.Serializable]
public class EnemyData
{
    public bool Spawned;
    public string type;
    public float delay;
}