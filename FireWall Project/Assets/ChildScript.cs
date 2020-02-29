using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript : MonoBehaviour
{

    public GameObject parentObject;
    public float cameraSpeed;

    void Start()
    {
        this.transform.parent = parentObject.transform;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, parentObject.transform.position, cameraSpeed * Time.deltaTime);
    }

}