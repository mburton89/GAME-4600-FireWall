using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HectorATakeDamage : MonoBehaviour
{
    private HectorA _controller;
    [SerializeField] private float _damageToTake;

    public void Init(HectorA controller)
    {
        _controller = controller;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //_controller.TakeDamage();
    }
}
