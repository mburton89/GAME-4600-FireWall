using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _attack;
    [SerializeField] private AudioSource _takeDamage;

    public void PlayAttackSound()
    {
        _attack.Play();
    }

    public void PlayTakeDamageSound()
    {
        _takeDamage.Play();
    }
}