using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author Keiron
/// Moves bullets fired by turrets
/// </summary>
public class TravelPath : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 100.0f)]
    private float BulletSpeed;

    private Vector2 _direction;
    private Rigidbody2D _rb;
    LayerMask _enemyLayer;
    Transform _target;

    public void Constructor(Transform pTarget)
    {
        _target = pTarget;
        _rb = GetComponent<Rigidbody2D>();
        _direction = ((Vector2)pTarget.transform.position - (Vector2)transform.position).normalized;
        _rb.velocity = _direction * BulletSpeed;
        _enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _enemyLayer)
        {
            if (collision.gameObject.transform == _target)
            {
                EnemyManager.DestroyEnemy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
