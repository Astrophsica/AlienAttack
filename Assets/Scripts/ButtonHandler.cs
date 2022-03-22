using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public void OnClick(Button btn)
    {
        var prefab = btn.GetComponent<PrefabDisplay>().GetPrefab();
        Placer.ObjectToPlace = prefab;
    }
}
