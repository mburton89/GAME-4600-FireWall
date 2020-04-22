using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ExplosionChunk _explosionChunkPrefab;
    private AudioSource _audioSource;
    public Color explosionColor;

    public void Splode()
    {
        //ScreenShaker.Instance.ShakeScreen();
        CinemachineCameraShaker.Instance.ShakeCamera(0.5f);
        _audioSource.Play();

        for (int i = 0; i < 12; i++)
        {
            ExplosionChunk explosionChunk = Instantiate(_explosionChunkPrefab, this.transform.position, this.transform.rotation, this.transform);
            explosionChunk.Init(explosionColor);
        }

        Destroy(gameObject, 5.5f);
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Splode();
    }
}
