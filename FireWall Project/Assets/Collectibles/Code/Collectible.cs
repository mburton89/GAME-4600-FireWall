using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public AudioClip collectSound;
    private bool _hasBeenCollected;

    private void Awake()
    {
        _hasBeenCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !_hasBeenCollected)
        {
            _hasBeenCollected = true;
            CollectibleSoundManager.Instance.PlaySound(collectSound);
            GetCollected();
            Destroy(gameObject);
        }
    }

    public abstract void GetCollected();
}