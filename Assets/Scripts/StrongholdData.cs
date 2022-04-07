using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
