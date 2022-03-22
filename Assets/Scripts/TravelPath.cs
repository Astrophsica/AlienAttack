using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelPath : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 100.0f)]
    private float BulletSpeed;

    private Vector2 _direction;
    private Rigidbody2D _rb;
    LayerMask _enemyLayer;

    public void Constructor(Vector2 pGoal)
    {
        _rb = GetComponent<Rigidbody2D>();
        _direction = (pGoal - (Vector2)transform.position).normalized;
        _rb.velocity = _direction * BulletSpeed;
        _enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _enemyLayer)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
