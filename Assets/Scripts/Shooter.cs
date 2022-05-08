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
    private float FireRate; //Measured in how many shots per second, i.e. 1 Firerate = 1 shot per second.

    [SerializeField]
    GameObject BulletPrefab;

    AudioSource _audioSource;
    Transform _transform;
    private float timeBetweenShots;
    private float counter;
    private LayerMask _enemyLayer;


    private void Start()
    {
        timeBetweenShots = 1.0f / FireRate;
        _enemyLayer = LayerMask.GetMask("Enemy");
        _transform = GetComponent<Transform>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        counter += Time.deltaTime;
        //This 'if' effectively limits the fire rate of turrets.
        if (counter < timeBetweenShots) { return; }
        counter = 0;

        Transform enemy = GetClosestEnemy();
        if (enemy == null) { return; }
        
        var projectile = Instantiate(BulletPrefab, _transform.position, Quaternion.identity);
        PlayAudioClip();
        Vector3 diff = enemy.position - _transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        var travelPathComponent = projectile.GetComponent<TravelPath>();
        //Runs a late constructor on the projectile's path component, passing it's goal destination i.e. enemy
        travelPathComponent.Constructor(enemy);
    }

    private void PlayAudioClip()
    {
        _audioSource.PlayOneShot(_audioSource.clip);
    }

    /// <summary>
    /// Runs a circle collision scan on the field, then returns the closest enemy to the turret.
    /// </summary>
    /// <returns></returns>
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
