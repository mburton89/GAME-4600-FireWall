﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [HideInInspector] public bool isApproachingWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Environment")
        {
            isApproachingWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Environment")
        {
            isApproachingWall = false;
        }
    }
}
