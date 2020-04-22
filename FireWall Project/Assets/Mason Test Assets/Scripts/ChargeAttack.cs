using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : MonoBehaviour
{
    public Rigidbody2D rb;
    public float secondsBeforeDash;
    public float dashSpeed;

    public void Dash(Vector3 target)
    {
        int directionModifier = 1;
        if (target.x < transform.position.x)
        {
            directionModifier = -1;
        }

        print("target.x: " + target.x);
        print("transform.position.x: " + transform.position.x);
        print("directionModifier: " + directionModifier);

        StartCoroutine(Dash(new Vector2(dashSpeed * directionModifier, 0)));
    }

    public void StartDash(Vector3 target)
    {
        int directionModifier = 1;
        if (target.x < transform.position.x)
        {
            directionModifier = -1;
        }
        
        rb.velocity = new Vector2(dashSpeed * directionModifier, 0);
    }

    public void EndDash()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Player")
        {
            //player.playerMaxHealth = player.playerMaxHealth - damage;
        }
    }

    private IEnumerator Dash(Vector2 velocity)
    {
        //TODO Show "wind-up" animation
        yield return new WaitForSeconds(secondsBeforeDash);
        rb.velocity = velocity;
        yield return new WaitForSeconds(0.17f);
        rb.velocity = Vector2.zero;
    }
}
