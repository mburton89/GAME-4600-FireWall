using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCollectible : Collectible
{
    [SerializeField] private float _percentageToRefilAmmo;

    public override void GetCollected()
    {
        Debug.LogWarning("Get Collected for " + gameObject.name + " not implemented");
        GunAndAmmoManager.Instance.RefillAmmoPercentage(_percentageToRefilAmmo);
    }
}
