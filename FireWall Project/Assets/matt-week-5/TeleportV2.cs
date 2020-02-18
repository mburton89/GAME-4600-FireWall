using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportV2 : MonoBehaviour
{
    private Transform PlayerTransform;

    public float distanceToTeleport;

    void Start()
    {
        PlayerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 teleportPosition = new Vector3(PlayerTransform.position.x + distanceToTeleport, PlayerTransform.position.y, 0);
            PlayerTransform.position = teleportPosition;
        }
    }
}
