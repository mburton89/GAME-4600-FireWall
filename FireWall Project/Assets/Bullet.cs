﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float damage = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            enemy.ApplyDamage(damage);
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }
}
