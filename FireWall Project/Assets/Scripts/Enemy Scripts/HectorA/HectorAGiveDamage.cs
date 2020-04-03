using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HectorAGiveDamage : MonoBehaviour
{
    [SerializeField] private float _damageToGive;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        collision.GetComponent<V3PlayerCharacterControler>().ApplyDamage(_damageToGive);
    //        print("HectorA caused " + _damageToGive + " damage.");
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<V3PlayerCharacterControler>().ApplyDamage(_damageToGive);
            print("HectorA caused " + _damageToGive + " damage.");
        }
    }
}
