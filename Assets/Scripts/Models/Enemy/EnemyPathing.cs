using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

// Author: Keiron Beadle
public class EnemyPathing
{
    Transform _target; //Target the AI will move to

    Transform _transform;
    public float _speed = 200f; //Speed of AI, this should be changed later to allow for different speeds
    public float _nextWaypointDistance = 0.1f; //Distance at which AI starts moving to next node in path
    Path _path; //Current path
    int _pathIndex = 0; 
    bool _reachedEndOfPath = false;
    Seeker _seeker; //Seeker component on AI


    public EnemyPathing(float speed, Transform transform, Seeker seeker)
    {
        _speed = speed;
        _transform = transform;
        _seeker = seeker;

        //Generate path
        if (_target is null) { return; }
        GenerateNewPath(_target);
    }

    public void SetTarget(Transform pTarget)
    {
        _target = pTarget;
        GenerateNewPath(_target);
    }

    void GenerateNewPath(Transform pTarget)
    {
        _seeker.StartPath(_transform.position, pTarget.position, OnPathComplete);
    }

    public void Update()
    {
        if (_path == null) return;
        _reachedEndOfPath = _pathIndex >= _path.vectorPath.Count;
        if (!_reachedEndOfPath){
            FollowPath();
        }
        else
        {
            if (Vector2.Distance(_target.position, _transform.position) > 0.2f)
            {
                GenerateNewPath(_target);
            }
        }
    }

    void FollowPath(){
        //get direction from current to next point, then normalise
        Vector3 direction = (_path.vectorPath[_pathIndex] - _transform.position).normalized;
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
