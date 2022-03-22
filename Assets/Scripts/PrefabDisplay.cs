using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PrefabDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject PrefabToDisplay;
    Texture2D _prefabTexture;

    void Start()
    {
        _prefabTexture = AssetPreview.GetAssetPreview(PrefabToDisplay);
        GetComponent<Image>().sprite = Sprite.Create(_prefabTexture, 
            new Rect(0,0, _prefabTexture.width, _prefabTexture.height), Vector2.zero);
    }

    public GameObject GetPrefab()
    {
        return PrefabToDisplay;
    }
}
