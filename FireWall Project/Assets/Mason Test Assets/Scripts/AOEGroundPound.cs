using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEGroundPound : MonoBehaviour
{
    public V3PlayerCharacterControler player;
    public Vector2 location;
    public float radius;
    public float damage;
    private BoxCollider2D _boxCollider2D;
    public float newRadius;
    public float originalRadius;
    public float poundDuration;
    private bool _hasPounded;

    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _hasPounded = false;
    }

    void Update()
    {

    }

    private IEnumerator PoundGround()
    {
        yield return new WaitForSeconds(poundDuration);
    }

    private void OnTriggerEnter2D (Collider2D otherObject)
    {
        Debug.Log("enter is Working");

        if (otherObject.tag == "Ground")
        {
            //_boxCollider2D. = radius;
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