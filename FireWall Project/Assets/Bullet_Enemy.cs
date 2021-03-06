﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Rigidbody2D rb;
    public float damage = 5f;

    private float receivedAngle;

    private bool lifespan = false;

    private Weapon weaponRef;

    private Vector3 _mousePos;
    private Transform _firePoint;

    void Awake()
    {
        weaponRef = GetComponent<Weapon>();
        rb = GetComponent<Rigidbody2D>();
        //receivedAngle = weaponRef.angle;
    }
    public ParticleSystem Spread;

    public void Init(Vector3 mousePos, Transform firePoint)
    {
        _mousePos = mousePos;
        _firePoint = firePoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 direction = weaponRef.mousePos - weaponRef.firePoint.transform.position;
        Vector3 direction = _mousePos - _firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //weaponRef.firePoint.transform.position = weaponRef.lookDir;
        //rb.velocity = weaponRef.lookDir * speed; //this permenantly sets bullet direction as right, needs to be changed to angle of mouse position
        //rb.AddForce(weaponRef.firePoint.up * speed, ForceMode2D.Impulse);
        //rb.rotation = angle;
        Destroy(gameObject, 1.2f);
    }

    void Update()
    {
        //BulletLifeSpan();

        //if (lifespan)
        //{
        //    Debug.Log("Destroying...");
        //    Destroy(gameObject);
        //}
    }

    //IEnumerator BulletLifeSpan()
    //{
    //    Debug.Log("Coroutine reached");
    //    yield return new WaitForSeconds(2f);
    //    lifespan = true;
    //    Debug.Log("Coroutine ended");
    //}

    public float getSpeed()
    {
        return speed;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            V3PlayerCharacterControler playerChar = collision.GetComponent<V3PlayerCharacterControler>();
            playerChar.ApplyDamage(damage);
            Destroy(gameObject);
            
        }

        if(collision.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
            
        }

        if (Spread != null)
        {
            Spread.GetComponent<ParticleSystem>().Play();
        }
        
    }
}
