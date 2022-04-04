using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private int Health;

    EnemyPathing _enemyPathing;
    LayerMask _strongholdLayer;
    HealthManager _healthManager;

    void Awake()
    {
        _enemyPathing = new EnemyPathing(Speed, GetComponent<Transform>(), GetComponent<Seeker>());
        _strongholdLayer = LayerMask.NameToLayer("Stronghold");
        _healthManager = GameObject.Find("HealthManager").gameObject.GetComponent<HealthManager>();
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
            _healthManager.ReduceHealth(Health);
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
