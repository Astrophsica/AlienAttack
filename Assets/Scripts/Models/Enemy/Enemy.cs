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
        // Create new enemy pathing and pass relevant properties needed for pathing
        _enemyPathing = new EnemyPathing(Speed, GetComponent<Transform>(), GetComponent<Seeker>());
        _strongholdLayer = LayerMask.NameToLayer("Stronghold");
    }

    public void SetTarget(Transform pTarget)
    {
        // Set enemy destination/target
        _enemyPathing.SetTarget(pTarget);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collision with stronghold/base occurs, then damage base based on enemy health
        if (collision.gameObject.layer == _strongholdLayer)
        {
            // Damage based on health of enemy
            HealthManager.ReduceHealth(Health);
            // Destroy itself since it has damaged stronghold/base
            EnemyManager.DestroyEnemy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        // Update enemy pathing on fixed timing
        _enemyPathing.Update();
    }
}
