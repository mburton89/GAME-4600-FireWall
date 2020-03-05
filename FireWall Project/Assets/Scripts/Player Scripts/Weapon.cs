using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    CharacterController2D entity;

    public Vector3 mousePos;
    public Camera cam;

    public Rigidbody2D armRB;

    private GameObject bulletInstance;
    public float angle;

    Vector3 anchorLocation;

    public Vector3 lookDir;

    public GameObject pivot;

    // Update is called once per frame
    void Update()
    {

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - pivot.transform.position; //changed from defining it here to above. may do unexpected things?
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; //changed from defining it here to above. may do unexpected things?
        armRB.rotation = angle;
        pivot.transform.eulerAngles = new Vector3(0,0,angle);

        //firePoint.position = lookDir;

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
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        float bulletSpeed = bulletInstance.GetComponent<Bullet>().speed;

        //Quaternion angle2 = new Quaternion();
        //angle2.SetFromToRotation(transform.rotation, mousePos);

        float xAndYSum = Mathf.Abs(mousePos.x) + Mathf.Abs(mousePos.y);
        float mouseX = mousePos.x / xAndYSum;
        float mouseY = mousePos.y / xAndYSum;
        Vector3 newMouseDirection = new Vector3(mouseX, mouseY, 0);
        rb.AddForce(newMouseDirection * bulletSpeed, ForceMode2D.Impulse); //firePoint.Up somehow needs to be the rotation
        //StartCoroutine(shootingSlowdown());
        //Debug.Log("go to next coroutine");
        //StartCoroutine(BulletLifeSpan());
        //Debug.Log("should be preceeded by 'Coroutine ended'");
    }
}
