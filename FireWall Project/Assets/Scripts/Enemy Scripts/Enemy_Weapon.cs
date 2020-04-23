using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon : MonoBehaviour
{
    public Transform firePoint;
    public Bullet_Enemy bulletPrefab;
    CharacterController2D entity;

    public Vector3 mousePos;
    public Camera cam;

    public Rigidbody2D armRB;

    private GameObject bulletInstance;
    public float angle;

    Vector3 anchorLocation;

    public Vector3 lookDir;

    public GameObject pivot;

    [SerializeField] private AudioSource _audioSource;

    Vector3 charTarget;
    bool canShoot;

    [SerializeField] Trojan_Archer_Controller temp;

    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float nextFire = 0.0f;

    // Update is called once per frame
    void Update()
    {
        canShoot = temp.getPlayerFound();                                                                                                   //PROBLEM
        charTarget = GameObject.Find("Player").transform.position;
        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = charTarget - pivot.transform.position; //changed from defining it here to above. may do unexpected things?
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; //changed from defining it here to above. may do unexpected things?
        armRB.rotation = angle;
        pivot.transform.eulerAngles = new Vector3(0,0,angle);

        //firePoint.position = lookDir;

        //firePoint.rotation = angle;

        //anchorLocation = GameObject.Find("Arm Anchor").transform.position;
        //armRB.position = anchorLocation;

        //This fires autonomously, so remove mouse logic
        if (canShoot && (Time.time > nextFire))
        {
            Debug.Log("this is actually firing");
            nextFire = Time.time + fireRate;
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
        ////Logic to shoot
        //Bullet_Enemy bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //bulletInstance.Init(mousePos, firePoint);
        //Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        //float bulletSpeed = bulletInstance.GetComponent<Bullet_Enemy>().speed;

        ////Quaternion angle2 = new Quaternion();
        ////angle2.SetFromToRotation(transform.rotation, mousePos);

        ////OKAY so i figure something will get fucked up here
        //float xAndYSum = Mathf.Abs(charTarget.x) + Mathf.Abs(charTarget.y); //I CHANGED THIS TO POSITION.X and POSITION.Y hopefully that is the equivalent
        //float mouseX = charTarget.x / xAndYSum; //see above
        //float mouseY = charTarget.y / xAndYSum; //see above
        //Vector3 newMouseDirection = new Vector3(mouseX, mouseY, 0);
        //rb.AddForce(newMouseDirection * bulletSpeed, ForceMode2D.Impulse); //firePoint.Up somehow needs to be the rotation
        ////StartCoroutine(shootingSlowdown());
        ////Debug.Log("go to next coroutine");
        ////StartCoroutine(BulletLifeSpan());
        ////Debug.Log("should be preceeded by 'Coroutine ended'");

        //_audioSource.Play();

        //Determine Direction to Shoot //fix this so its not mouse position
        var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        var direction = worldMousePosition - transform.position;
        Vector3 directionToShoot = direction.normalized;

        //Create Bullet
        Bullet_Enemy bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletInstance.Init(directionToShoot, firePoint);

        //Get Bullet Components
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        float bulletSpeed = bulletInstance.GetComponent<Bullet_Enemy>().speed;

        //Apply Force
        rb.AddForce(directionToShoot * bulletSpeed, ForceMode2D.Impulse);

        //Play Sound
        _audioSource.Play();

        //GunAndAmmoManager.Instance.DeductAmmoPercentage(_energyUsedPerShot); //TODO IF PLAYER
    }
}
