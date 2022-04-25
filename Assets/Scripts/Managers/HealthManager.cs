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
            Health = Mathf.Max(0, Health-amount); //Stops health going negative 
            HealthChanged.Invoke(Health);
        }
        if (Health == 0)
        {
            // Game over
            HealthIsZero.Invoke();
        }
    }
}
