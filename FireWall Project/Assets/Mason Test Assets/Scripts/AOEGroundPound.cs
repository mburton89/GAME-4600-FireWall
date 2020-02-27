using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEGroundPound : MonoBehaviour
{
    public V3PlayerCharacterControler player;
    public Vector2 location;
    public float radius;
    public float damage;
    private CircleCollider2D cC;
    public float newRadius;
    public float originalRadius;


    // Start is called before the first frame update
    void Start()
    {
        cC = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
     
        
    }

    private void OnTriggerEnter2D (Collider2D otherObject)
    {
        Debug.Log("enter is Working");

        if (otherObject.tag == "Ground")
        {
            cC.radius = radius;
            Debug.Log("ground hit");
        }

        if (otherObject.tag == "Player" /*&& cC.radius == newRadius*/)
        {
            Debug.Log("player hit");
            //player.playerMaxHealth = player.playerMaxHealth - damage;
        }
    }

    private void OnTriggerExit2D(Collider2D otherObject)
    {
        Debug.Log("exit is Working");

        if (otherObject.tag == "Ground")
        {
            //cC.radius = originalRadius;
            Debug.Log("radius returned to normal");
        }
    }

}