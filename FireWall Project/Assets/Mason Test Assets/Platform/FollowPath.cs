﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FollowPath : MonoBehaviour
{
    public enum FollowType
    { 
        MoveTowards,
        Lerp
    }

    public FollowType Type = FollowType.MoveTowards;
    public PathDefinition Path;
    public float speed = 1;
    public float MaxDistanceToGoal = .1f;

    private IEnumerator<Transform> _currentPoint;



    // Start is called before the first frame update
   public void Start()
    {
        if (Path == null)
        {
            Debug.LogError("Path cannot be null", gameObject);
            return;
        }
        _currentPoint = Path.GetPathEnumerator();
        _currentPoint.MoveNext();

        if (_currentPoint.Current == null)
        {
            return;
        }

        transform.position = _currentPoint.Current.position;
    }

    // Update is called once per frame
    public void Update()
    {
        if (_currentPoint == null || _currentPoint.Current == null)
        {
            return;
        }

        if (Type == FollowType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * speed);
        }
        else if (Type == FollowType.Lerp)
        { 
            transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * speed);
        }

        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            _currentPoint.MoveNext();
        }
    }
}
