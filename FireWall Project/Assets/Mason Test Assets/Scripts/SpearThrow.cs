using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearThrow : MonoBehaviour
{
    public GameObject projectile;
    public Transform PlayerPosition;
    public Transform spawn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            spawnSpear();
        }
    }

    private void spawnSpear()
    {
        GameObject spear = Instantiate(projectile, spawn.transform.position, Quaternion.identity) as GameObject;
        

    }
}
