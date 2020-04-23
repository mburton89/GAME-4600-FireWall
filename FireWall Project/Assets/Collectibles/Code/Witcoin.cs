using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witcoin : Collectible
{
    public override void GetCollected()
    {
        CoinManager.Instance.IncrementCollectedCoinCount();
    }
}
