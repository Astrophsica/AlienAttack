using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HealthManager : MonoBehaviour
{
    public int _health = 100;
    public IntEvent _healthChanged;
    public UnityEvent _healthIsZero;

    // Runs before start() on all game objects
    void Awake()
    {
        _healthChanged = new IntEvent();
        _healthIsZero = new UnityEvent();
    }

    public void ReduceHealth(int amount)
    {
        if (amount > 0)
        {
            _health -= amount;
            _healthChanged.Invoke(_health);
        }
        if (amount == 0)
        {
            // Game over
            _healthIsZero.Invoke();
        }
    }
}
