using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    IEnumerator shootingSlowdown()
    {
        yield return new WaitForSeconds(1f);
    }

    void Shoot()
    {
        //Logic to shoot
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(shootingSlowdown());
    }
}
