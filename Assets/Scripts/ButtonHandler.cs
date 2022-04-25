using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    Placer placer;

    public void OnClick(Button btn)
    {
        var prefab = btn.GetComponent<PrefabDisplay>().GetPrefab();
        placer.ObjectToPlace = prefab;
    }
}
