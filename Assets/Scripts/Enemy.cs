using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float _speed;
    public int _health;
    EnemyPathing _enemyPathing;
    LayerMask _strongholdLayer;
    HealthManager _healthManager;

    // Start is called before the first frame update
    void Start()
    {
        _speed = 200f;
        _health = 1;
        _enemyPathing = new EnemyPathing(_speed, GetComponent<Transform>(), GetComponent<Seeker>());
        _strongholdLayer = LayerMask.NameToLayer("Stronghold");
        _healthManager = GameObject.Find("HealthManager").gameObject.GetComponent<HealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _strongholdLayer)
        {
            _healthManager.ReduceHealth(1);
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
