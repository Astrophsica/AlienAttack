using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] EnemyTypes;

    public List<GameObject> enemies = new List<GameObject>();

    public GameObject SpawnNewEnemy(string type, Vector3 position)
    {
        var enemy = Instantiate(EnemyTypes[0], position, Quaternion.identity);
        enemies.Add(enemy);
        return enemy;
    }

    public void DestroyEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }
}
