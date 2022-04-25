using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Author: Humza Khan
public class MoneyTextUI : MonoBehaviour
{
    TextMeshProUGUI text;
    [Tooltip("Currency sign to display")]
    public string CurrencySign;

    void Start()
    {
        // Get text and set to money amount
        text = GetComponent<TextMeshProUGUI>();
        text.text = CurrencySign + ShopManager.Money;

        // Set UpdateMoneyText method to be called when money changes
        ShopManager.MoneyChanged.AddListener(UpdateMoneyText);
    }

    void UpdateMoneyText(int money)
    {
        // Set new money text
        text.text = CurrencySign + money;
    }
}
