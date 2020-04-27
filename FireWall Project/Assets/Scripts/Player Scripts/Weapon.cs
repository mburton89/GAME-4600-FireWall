using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private V3PlayerCharacterControler _player;

    public Transform firePoint;
    public Bullet bulletPrefab;
    CharacterController2D entity;

    public Vector3 mousePos;
    public Camera cam;

    private GameObject bulletInstance;
    public float angle;

    Vector3 anchorLocation;

    public Vector3 lookDir;

    public GameObject pivot;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float _energyUsedPerShot;

    private bool _switchedToRight;

    [SerializeField] private Transform _arm;
    [SerializeField] private Transform _gun;

    private void Awake()
    {
        _switchedToRight = true;
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - pivot.transform.position; //changed from defining it here to above. may do unexpected things?
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; //changed from defining it here to above. may do unexpected things?
        pivot.transform.eulerAngles = new Vector3(0,0,angle);

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (GunAndAmmoManager.Instance.currentAmmoPercentage > 0)
            {
                Shoot();
            }
            else
            {
                GunAndAmmoManager.Instance.ShowNoAmmoWarning();
            }
        }

        //if (!_switchedToRight && _player.controller.getIsFacingRight())
        //{
        //    _arm.transform.localEulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        //    _switchedToRight = true;
        //    print(" _switchedToRight = true;");
        //}
        //else if (_switchedToRight && !_player.controller.getIsFacingRight())
        //{
        //    _arm.transform.eulerAngles = new Vector3(-180, 0, transform.eulerAngles.z);
        //    _switchedToRight = false;
        //    print(" _switchedToRight = false;");
        //}
    }

    IEnumerator shootingSlowdown()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator BulletLifeSpan()
    {
        yield return new WaitForSeconds(2f);
    }

    void Shoot()
    {
        //Determine Direction to Shoot
        var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        var direction = worldMousePosition - transform.position;

        float xAndYSum = Mathf.Abs(direction.x) + Mathf.Abs(direction.y);
        float mouseX = direction.x / xAndYSum;
        float mouseY = direction.y / xAndYSum;
        Vector3 newMouseDirection = new Vector3(mouseX, mouseY, 0);

        //Vector3 directionToShoot = direction.normalized;
        //print(directionToShoot);

        //Create Bullet
        Bullet bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletInstance.Init(newMouseDirection, firePoint);

        //Get Bullet Components
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        float bulletSpeed = bulletInstance.GetComponent<Bullet>().speed;

        
        rb.AddForce(newMouseDirection * bulletSpeed, ForceMode2D.Impulse); //firePoint.Up somehow needs to be the rotation

        //Apply Force
        //rb.AddForce(directionToShoot * bulletSpeed, ForceMode2D.Impulse);

        //Play Sound
        _audioSource.Play();

        GunAndAmmoManager.Instance.DeductAmmoPercentage(_energyUsedPerShot); //TODO IF PLAYER
    }

    public void Hide()
    {
        _arm.localScale = new Vector3(0, 1, 0);
        _gun.localScale = new Vector3(0, 1, 0);
    }

    public void Show()
    {
        _arm.localScale = Vector3.one;
        _gun.localScale = Vector3.one;
    }
}
