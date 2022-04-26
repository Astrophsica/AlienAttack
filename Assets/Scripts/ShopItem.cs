using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Authors: Humza Khan and Keiron Beadle
public class ShopItem : MonoBehaviour
{
    [Tooltip("Price of item")]
    [SerializeField]
    public int Price;

    [Tooltip("Game object prefab for this item")]
    [SerializeField]
    public GameObject Prefab;

    [Tooltip("The player link object with property to Player object")]
    [SerializeField]
    public GameObject PlayerLink;

    public void Start()
    {
        // Sets OnClick to be called on button click
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        // Sets SelectedShopItem and ObjectToPlace for placer script
        PlayerLink.GetComponent<PlayerLink>().Player.GetComponent<Placer>().SelectedShopItem = this;
        PlayerLink.GetComponent<PlayerLink>().Player.GetComponent<Placer>().ObjectToPlace = Prefab;
    }
}
