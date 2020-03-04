using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sript : MonoBehaviour
{
    public ParticleSystem _psystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        _psystem = GetComponent<ParticleSystem>();
    }
    void OnTriggerEnter(Collider col)
    {

        _psystem.Play();

    }
}
