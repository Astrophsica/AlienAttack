using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    int _health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ReduceHealth(int amount)
    {
        _health -= amount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
