using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

// Author: Humza Khan
public class EnemyManager : MonoBehaviour
{
    [Tooltip("List of unique enemies")]
    GameObject[] EnemyTypes;

    static public List<GameObject> enemies = new List<GameObject>();
    static public Dictionary<string, GameObject> enemyAssets = new Dictionary<string, GameObject>();

    static public GameObject SpawnNewEnemy(string type, Vector3 position)
    {
        // Get enemy object to spawned based on type
        GameObject enemyObjectToSpawn = null;
        switch (type)
        {
            case "basic":
                enemyObjectToSpawn = LoadPrefabFromFile("Test_Enemy");
                break;
        }
        
        // Create enemy and add to enemies list
        var enemy = Instantiate(enemyObjectToSpawn, position, Quaternion.identity);
        enemies.Add(enemy);
        return enemy;
    }

    static public void DestroyEnemy(GameObject enemy)
    {
        // Remove enemy from enemies list and then destroy enemy object
        enemies.Remove(enemy);
        Destroy(enemy);
    }

    static private GameObject LoadPrefabFromFile(string filename)
    {
        // Check if enemy object already loaded in. If so, return that object
        GameObject loadedObject;
        if (enemyAssets.TryGetValue(filename, out loadedObject))
        {
            return loadedObject;
        }

        // Get object from file, add to asset list and return object
        loadedObject = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Game/Enemy/" + filename + ".prefab", typeof(GameObject));
        if (loadedObject == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        enemyAssets.Add(filename, loadedObject);
        return loadedObject;
    }
}
