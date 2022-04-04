using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HealthManager : MonoBehaviour
{
    static public int Health = 100;
    static public IntEvent HealthChanged;
    static public UnityEvent HealthIsZero;

    // Runs before start() on all game objects
    void Awake()
    {
        HealthChanged = new IntEvent();
        HealthIsZero = new UnityEvent();
    }

    static public void ReduceHealth(int amount)
    {
        if (amount > 0)
        {
            Health -= amount;
            HealthChanged.Invoke(Health);
        }
        if (amount == 0)
        {
            // Game over
            HealthIsZero.Invoke();
        }
    }
}
