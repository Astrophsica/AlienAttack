using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Humza Khan
public class Enemy : MonoBehaviour
{
    [Tooltip("Movement speed of enemy")]
    [SerializeField]
    private float Speed;

    [Tooltip("Health of enemy")]
    [SerializeField]
    private int Health;

    EnemyPathing _enemyPathing;
    LayerMask _strongholdLayer;

    void Awake()
    {
        _enemyPathing = new EnemyPathing(Speed, GetComponent<Transform>(), GetComponent<Seeker>());
        _strongholdLayer = LayerMask.NameToLayer("Stronghold");
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetTarget(Transform pTarget)
    {
        _enemyPathing.SetTarget(pTarget);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _strongholdLayer)
        {
            // Damage based on health of enemy
            HealthManager.ReduceHealth(Health);
        }
    }

    private void FixedUpdate()
    {
        _enemyPathing.Update();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
