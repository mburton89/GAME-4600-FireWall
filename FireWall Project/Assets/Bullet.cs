using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float damage = 5f;

    private float receivedAngle;

    private bool lifespan = false;

    private Weapon weaponRef;

    void Awake()
    {
        weaponRef = GetComponent<Weapon>();
        //receivedAngle = weaponRef.angle;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 direction = weaponRef.mousePos - weaponRef.firePoint.transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //weaponRef.firePoint.transform.rotation = angle;
        //rb.velocity = weaponRef.lookDir * speed; //this permenantly sets bullet direction as right, needs to be changed to angle of mouse position
        //rb.AddForce(weaponRef.firePoint.up * speed, ForceMode2D.Impulse);
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
