using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathDefinition : MonoBehaviour
{
    public Transform[] Points; // array to store points

    public IEnumerator<Transform> GetPathEnumerator()
    {
        //throw new NotImplementedException();
        if (Points == null || Points.Length < 2)
        {
            yield break;
        }

            var direction = 1; // start point
            var index = 0; // array start index
        while (true) // loop never stops due to true
        {
            yield return Points[index]; // returns index

            if (Points.Length == 1)
            {
                continue;
            }

            if (index <= 0) // changes direction
            {
                direction = 1;
            }
            else if (index >= Points.Length - 1) //reverses direction
            {
                direction = -1;
            }

            index = index + direction; //moves to next point
        }
        
        
    }

    public void OnDrawGizmos() // used for drawing line for path
    {
        if (Points == null || Points.Length < 2) // checks if pointd array has enough points to execute
        {
            return;
        }

        for (var i = 1; i < Points.Length; i++)
        {
            Gizmos.DrawLine(Points[i-1].position, Points[i].position); // used to draw line from point to point
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
