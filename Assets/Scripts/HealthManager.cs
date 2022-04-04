using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    int _health = 100;
    public UnityEvent _HealthIsZero;

    // Start is called before the first frame update
    void Start()
    {
        _HealthIsZero = new UnityEvent();
    }

    public void ReduceHealth(int amount)
    {
        if (amount > 0)
        {
            _health -= amount;
        }
        if (amount == 0)
        {
            // Game over
            _HealthIsZero.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
