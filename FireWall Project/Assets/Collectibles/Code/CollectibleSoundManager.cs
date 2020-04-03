using UnityEngine;

public class CollectibleSoundManager : MonoBehaviour
{
    public static CollectibleSoundManager Instance;
    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip collectSound)
    {
        _audioSource.clip = collectSound;
        _audioSource.Play();
    }
}
