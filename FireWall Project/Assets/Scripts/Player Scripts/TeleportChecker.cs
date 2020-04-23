using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportChecker : MonoBehaviour
{
    [HideInInspector] public bool canTeleport;

    private void Awake()
    {
        canTeleport = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Environment")
        {
            canTeleport = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Environment")
        {
            canTeleport = true;
        }
    }
}
