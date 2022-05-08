using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author Keiron
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

    //Vec3 intialised as 0,0,0, we won't be making any maps in the range [-999,-999] so it's a fine bound to use
    private Vector3 strongholdPosition = new Vector3(-999,-999); 

    /// <summary>
    /// Runs a check on all enemy spawn points to ensure they can all reach the stronghold
    /// If one is found to be unable to reach this destination, true is returned.
    /// </summary>
    /// <returns></returns>
    public bool IsCutoff()
    {
        if (enemySpawnPoints == null)
        {
            var waveManager = FindObjectOfType<WaveManager>();
            enemySpawnPoints = waveManager.SpawnPoints;
        }
        if (strongholdPosition.x == -999 && strongholdPosition.y == -999)
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
