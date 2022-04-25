using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Author: Humza Khan
public class ShopManager : MonoBehaviour
{
    static public int Money = 100;
    static public IntEvent MoneyChanged;

    // Runs before start() on all game objects
    void Awake()
    {
        // Create Int event
        MoneyChanged = new IntEvent();
    }

    static public void SpendMoney(int amount)
    {
        // Check if amount is positive and won't set money to negative
        if (amount > 0 && Money - amount >= 0)
        {
            Money -= amount;

            MoneyChanged.Invoke(Money);
        }
    }

    static public void AddMoney(int amount)
    {
        // Check if amount is positive
        if (amount > 0)
        {
            Money += amount;

            MoneyChanged.Invoke(Money);
        }
    }
}
