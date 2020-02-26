using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    CharacterController2D entity;

    Vector2 mousePos;
    public Camera cam;

    public Rigidbody2D armRB;

    private GameObject bulletInstance;

    Vector3 anchorLocation;

    // Update is called once per frame
    void Update()
    {

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - armRB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        armRB.rotation = angle;

        //firePoint.rotation = angle;

        //anchorLocation = GameObject.Find("Arm Anchor").transform.position;
        //armRB.position = anchorLocation;

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("Shot");
            Shoot();
            Debug.Log("Shoot enum completed");
            //BulletLifeSpan();
           
        }
    }

    IEnumerator shootingSlowdown()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator BulletLifeSpan()
    {
        Debug.Log("Coroutine reached");
        yield return new WaitForSeconds(2f);
        //Destroy(bulletInstance);
        Debug.Log("Coroutine ended");
    }

    void Shoot()
    {
        //Logic to shoot
        bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(shootingSlowdown());
        Debug.Log("go to next coroutine");
        StartCoroutine(BulletLifeSpan());
        Debug.Log("should be preceeded by 'Coroutine ended'");
    }
}
