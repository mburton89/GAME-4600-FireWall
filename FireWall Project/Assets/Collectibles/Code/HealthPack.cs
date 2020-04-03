using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : Collectible
{
    [SerializeField] private int healthToGive;

    public override void GetCollected()
    {
        FindObjectOfType<V3PlayerCharacterControler>().AddHealth(healthToGive);
    }
}
