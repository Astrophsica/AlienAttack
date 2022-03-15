using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField]
    Transform _target; //Target the AI will move to

    Transform _transform;
    public float _speed = 200f; //Speed of AI, this should be changed later to allow for different speeds
    public float _nextWaypointDistance = 0.1f; //Distance at which AI starts moving to next node in path
    Path _path; //Current path
    int _pathIndex = 0; 
    bool _reachedEndOfPath = false;
    Seeker _seeker; //Seeker component on AI

    void Start()
    {
        _transform = GetComponent<Transform>();
        _seeker = GetComponent<Seeker>();

        //Generate path
        _seeker.StartPath(_transform.position, _target.position, OnPathComplete);
    }

    void FixedUpdate()
    {
        if (_path == null) return;
        _reachedEndOfPath = _pathIndex >= _path.vectorPath.Count;
        if (!_reachedEndOfPath){
            FollowPath();
        }
    }

    void FollowPath(){
        //get direction from current to next point, then normalise
        Vector3 direction = (_path.vectorPath[_pathIndex] - transform.position).normalized;
        //add direction to position, multiplying by speed and deltaT
        _transform.position += direction * _speed * Time.deltaTime;

        float distanceToNextPoint = Vector2.Distance(_transform.position, _path.vectorPath[_pathIndex]);
        if (distanceToNextPoint < _nextWaypointDistance){
            _pathIndex++;
        }
    }

    //Called when path is finished computing
    private void OnPathComplete(Path p)
    {
        if (p.error){
            Debug.LogError("Error generating path: " + p.errorLog);
            return;
        }
        _path = p;
        _pathIndex = 0;
    }
}
