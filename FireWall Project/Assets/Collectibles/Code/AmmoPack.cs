using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : Collectible
{
    [SerializeField] private float _percentageToRefilAmmo;

    public override void GetCollected()
    {
        if (GunAndAmmoManager.Instance != null)
        {
            GunAndAmmoManager.Instance.RefillAmmoPercentage(_percentageToRefilAmmo);
        }
        else
        {
            Debug.LogWarning("Add GunAndAmmoManager To Your Scene!");
        }
    }
}
