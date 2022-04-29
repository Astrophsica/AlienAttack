using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Author: Humza Khan
public class HealthManager : MonoBehaviour
{
    static public int Health = 100;
    static public IntEvent HealthChanged;
    static public UnityEvent HealthIsZero;

    // Runs before start() on all game objects
    void Awake()
    {
        // Create Int event and no parameter event
        HealthChanged = new IntEvent();
        HealthIsZero = new UnityEvent();
    }

    static public void ReduceHealth(int amount)
    {
        // If amount is valid, then reduce health
        if (amount > 0)
        {
            Health = Mathf.Max(0, Health-amount); //Stops health going negative (Keiron Beadle)
            HealthChanged.Invoke(Health);
        }
        // If health is zero, then invoke HealthIsZero event
        if (Health == 0)
        {
            // Game over
            HealthIsZero.Invoke();
        }
    }
}
