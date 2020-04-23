using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundSpike : MonoBehaviour
{
    [SerializeField] private float _secondsToLift;
    [SerializeField] private float _secondsToDisapear;
    [SerializeField] private float _topYPosition;
    [SerializeField] private float _damageToGive;

    void Start()
    {
        Erupt();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<V3PlayerCharacterControler>().ApplyDamage(_damageToGive);
            print("GroundSpike caused " + _damageToGive + " damage.");
        }
    }

    void Erupt()
    {
        transform.DOMoveY(transform.position.y + _topYPosition, _secondsToLift).OnComplete(Retract);
    }

    void Retract()
    {
        transform.DOMoveY(transform.position.y - _topYPosition, _secondsToDisapear);
    }
}
