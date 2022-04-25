using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    public int Price;

    [SerializeField]
    public GameObject Prefab;

    [SerializeField]
    public PlayerLink PlayerLink;

    public void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        PlayerLink.placer.ObjectToPlace = Prefab;
    }
}
