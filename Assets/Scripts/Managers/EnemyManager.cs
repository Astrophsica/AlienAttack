using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] EnemyTypes;

    static public List<GameObject> enemies = new List<GameObject>();
    static public Dictionary<string, GameObject> enemyAssets = new Dictionary<string, GameObject>();

    static public GameObject SpawnNewEnemy(string type, Vector3 position)
    {
        GameObject enemyObjectToSpawn = null;
        switch (type)
        {
            case "basic":
                enemyObjectToSpawn = LoadPrefabFromFile("Test_Enemy");
                break;
        }
        
        var enemy = Instantiate(enemyObjectToSpawn, position, Quaternion.identity);
        enemies.Add(enemy);
        return enemy;
    }

    static public void DestroyEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }

    static private GameObject LoadPrefabFromFile(string filename)
    {
        GameObject loadedObject;
        if (enemyAssets.TryGetValue(filename, out loadedObject))
        {
            return loadedObject;
        }

        Debug.Log("Trying to load Prefab from file (" + filename + ")...");
        loadedObject = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Game/Enemy/" + filename + ".prefab", typeof(GameObject));
        if (loadedObject == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        enemyAssets.Add(filename, loadedObject);
        return loadedObject;
    }
}
