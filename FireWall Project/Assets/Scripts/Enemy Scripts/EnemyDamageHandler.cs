using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<V3PlayerCharacterControler>()) //TODO: This should check to see if the collision has a PlayerCharacterHealth script
        {
            HealthBar.Instance.DecreaseHealth(0.05f); //TODO: This should decrement from the players health, and not talk directly to Health Bar
        }
    }
}
