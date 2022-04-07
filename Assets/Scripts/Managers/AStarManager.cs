using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour
{
    private static AStarManager _instance;
    public static AStarManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (new GameObject("AStarManager_Container")).AddComponent<AStarManager>();
            }
            return _instance;
        }
    }

    private GameObject[] enemySpawnPoints;
    private Vector3 strongholdPosition;

    public bool IsCutoff()
    {
        if (enemySpawnPoints == null)
        {
            var waveManager = FindObjectOfType<WaveManager>();
            enemySpawnPoints = waveManager.SpawnPoints;
        }
        if (strongholdPosition == null)
        {
            strongholdPosition = FindObjectOfType<StrongholdData>().Position;
        }
        AstarPath.active.Scan();
        var node = AstarPath.active.GetNearest(strongholdPosition).node;
        foreach (var point in enemySpawnPoints)
        {
            var enemyNode = AstarPath.active.GetNearest(point.transform.position, NNConstraint.None).node;
            if (!PathUtilities.IsPathPossible(node, enemyNode))
            {
                return true;
            }
        }
        return false;
    }

    
}
