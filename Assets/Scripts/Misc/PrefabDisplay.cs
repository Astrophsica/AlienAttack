using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PrefabDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject PrefabToDisplay;

    void Start()
    {

    }

    public GameObject GetPrefab()
    {
        return PrefabToDisplay;
    }
}
