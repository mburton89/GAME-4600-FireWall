using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowBG : MonoBehaviour
{
    [SerializeField] private float _pulseSpeed;
    [SerializeField] private float _amountToExpand;
    [SerializeField] private float _amountToFade;

    void Start()
    {
        
    }

    private IEnumerator pulse()
    {
 
        yield return new WaitForSeconds(_pulseSpeed);

    }
}
