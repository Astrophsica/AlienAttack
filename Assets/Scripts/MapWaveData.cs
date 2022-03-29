using System;
using UnityEngine;
using UnityEngine.Serialization;

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
    public EnemyData[] enemies;
}

[System.Serializable]
public class EnemyData
{
    public string type;
    public float delay;
}