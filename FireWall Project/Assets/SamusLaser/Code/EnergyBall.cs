using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            enemy.ApplyDamage(damage);
        }

        if (collision.gameObject.tag == "SimpleEnemy")
        {
            Enemy enemy = collision.GetComponentInParent<Enemy>();
            enemy.TakeDamage(damage);
        }

        if (collision.gameObject.tag == "Boss")
        {
            HectorA enemy = collision.GetComponentInParent<HectorA>(); //TODO MWB: make this work for all bosses
            enemy.ApplyDamage(damage);
        }
    }
}
