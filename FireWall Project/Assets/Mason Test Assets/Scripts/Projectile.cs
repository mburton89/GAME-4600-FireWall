using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Vector2 target;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        Debug.Log("enter is Working");

        if (otherObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

        if (otherObject.tag == "Player")
        {
            //player.playerMaxHealth = player.playerMaxHealth - damage;
            Destroy(gameObject);
        }
    }
}
