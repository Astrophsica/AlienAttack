using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    [Range(1, 20)]
    private float AreaOfView;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float FireRate;

    [SerializeField]
    GameObject BulletPrefab;

    private float timeBetweenShots;
    private float counter;
    private LayerMask _enemyLayer;

    private void Start()
    {
        timeBetweenShots = FireRate / 1.0f;
        _enemyLayer = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        counter += Time.deltaTime;
        if (counter < timeBetweenShots) { return; }
        counter = 0;

        Transform enemy = GetClosestEnemy();
        if (enemy == null) { return; }

        var projectile = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        var travelPathComponent = projectile.GetComponent<TravelPath>();
        travelPathComponent.Constructor((Vector2)enemy.position);
    }

    private Transform GetClosestEnemy()
    {
        var hits = Physics2D.CircleCastAll(transform.position, AreaOfView, Vector2.zero, 0, _enemyLayer);
        if (hits.Length == 0) { return null; }
        float lowestDistance = float.MaxValue;
        Transform lowestEnemy = null;
        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(hit.transform.position, transform.position);
            if (dist < lowestDistance)
            {
                lowestDistance = dist;
                lowestEnemy = hit.collider.transform;
            }
        }
        return lowestEnemy;
    }
}
