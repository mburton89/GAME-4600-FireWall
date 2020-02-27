using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : MonoBehaviour
{
    public Rigidbody2D rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;

    public float speed;

    private Vector2 target;
    private Transform player;

    //private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        HandleDashBurton();
        playerLocation();
    }

    void playerLocation()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            target = new Vector2(player.position.x, 0);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                if (dashTime <= 0)
                {
                    //if (Input.GetKeyDown(KeyCode.Q))
                    //{
                        rb.velocity = rb.velocity * dashSpeed;
                        dashTime = startDashTime;
                    //}
                }
                else
                {
                    dashTime -= Time.deltaTime;
                }
        }
    }
    void HandleDashBurton()
    {
        if (dashTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                rb.velocity = rb.velocity * dashSpeed;
                dashTime = startDashTime;
            }
        }
        else
        {
            dashTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Player")
        {
            //player.playerMaxHealth = player.playerMaxHealth - damage;
        }
    }

}
