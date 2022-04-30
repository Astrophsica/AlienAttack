using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author Keiron
/// Allows turrets to shoot bullets / projectiles
/// </summary>
public class Shooter : MonoBehaviour
{
    [SerializeField]
    [Range(1, 20)]
    public float AreaOfView;

    [SerializeField]
    [Range(0.1f, 10.0f)]
    private float FireRate;

    [SerializeField]
    GameObject BulletPrefab;

    Transform _transform;
    private float timeBetweenShots;
    private float counter;
    private LayerMask _enemyLayer;


    private void Start()
    {
        timeBetweenShots = 1.0f / FireRate;
        _enemyLayer = LayerMask.GetMask("Enemy");
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        counter += Time.deltaTime;
        if (counter < timeBetweenShots) { return; }
        counter = 0;

        Transform enemy = GetClosestEnemy();
        if (enemy == null) { return; }
        
        var projectile = Instantiate(BulletPrefab, _transform.position, Quaternion.identity);
        Vector3 diff = enemy.position - _transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        var travelPathComponent = projectile.GetComponent<TravelPath>();
        travelPathComponent.Constructor(enemy);
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
