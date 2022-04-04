using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] EnemyTypes;

    static public List<GameObject> enemies = new List<GameObject>();

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
        Debug.Log("Trying to load Prefab from file (" + filename + ")...");
        var loadedObject = Resources.Load("Game/Enemy/" + filename);
        if (loadedObject == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        return (GameObject)loadedObject;
    }
}
