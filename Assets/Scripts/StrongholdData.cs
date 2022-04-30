using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author Keiron
/// Placed on the Stronghold object to get it's position for Wall checking
/// </summary>
public class StrongholdData : MonoBehaviour
{
    private Transform _transform;
    public Vector3 Position { get; private set; }

    void Start()
    {
        _transform = GetComponent<Transform>();
        Position = _transform.position;
    }
}
