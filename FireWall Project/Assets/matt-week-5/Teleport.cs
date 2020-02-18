using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Transform PlayerTransform;

    public Transform TeleportRight;
    public Transform TeleportUp;
    public Transform TeleportLeft;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerTransform.position = TeleportRight.position;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerTransform.position = TeleportUp.position;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerTransform.position = TeleportLeft.position;
        }
    }
}
