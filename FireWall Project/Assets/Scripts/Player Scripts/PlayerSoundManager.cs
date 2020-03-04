using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _run;
    [SerializeField] private AudioSource _jump;
    [SerializeField] private AudioSource _teleportStart;
    [SerializeField] private AudioSource _teleportEnd;

    [HideInInspector] public bool canMakeRunSound;

    private void Awake()
    {
        canMakeRunSound = true;
    }

    public void PlayRunSound()
    {
        _run.Play();
        canMakeRunSound = false;
    }

    public void StopRunSound()
    {
        _run.Stop();
        canMakeRunSound = true;
    }

    public void PlayJumpSound()
    {
        StopRunSound();
        _jump.Play();
    }

    public void PlayTeleportStartSound()
    {
        StopRunSound();
        _teleportStart.Play();
    }

    public void PlayTeleportEndSound()
    {
        StopRunSound();
        _teleportEnd.Play();
    }
}
