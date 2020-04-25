using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GunAndAmmoManager : MonoBehaviour
{
    public static GunAndAmmoManager Instance;

    [HideInInspector] public float currentAmmoPercentage;
    private float _maxAmmoPercentage;
    [SerializeField] private TextMeshProUGUI _currentAmmoPercentageText;

    [SerializeField] private Color _blue;
    [SerializeField] private Color _yellow;
    [SerializeField] private Color _red;

    private bool _canShowWarning;

    [SerializeField] private float _rechargeRate;

    void Awake()
    {
        Instance = this;
        _maxAmmoPercentage = 1f;
        currentAmmoPercentage = _maxAmmoPercentage;
        _canShowWarning = true;
    }

    private void Start()
    {
        UpdateText();
        InvokeRepeating("Recharge", _rechargeRate, _rechargeRate);
    }

    public void DeductAmmoPercentage(float percentageToDeduct)
    {
        currentAmmoPercentage -= percentageToDeduct;
        if (currentAmmoPercentage < 0f)
        {
            currentAmmoPercentage = 0f;
        }
        UpdateText();
    }

    public void RefillAmmoPercentage(float percentageToAdd)
    {
        currentAmmoPercentage += percentageToAdd;
        if (currentAmmoPercentage > 1f)
        {
            currentAmmoPercentage = 1f;
        }
        UpdateText();
    }

    void Recharge()
    {
        currentAmmoPercentage += 0.01f;
        if (currentAmmoPercentage > 1f)
        {
            currentAmmoPercentage = 1f;
        }
        UpdateText();
    }

    public void ShowNoAmmoWarning()
    {
        if (_canShowWarning)
        {
            StartCoroutine(ShowNoAmmoWarningCo());
        }
    }

    private IEnumerator ShowNoAmmoWarningCo()
    {
        _canShowWarning = false;
        transform.DOShakePosition(1, 3, 10, 90, false, true);
        yield return new WaitForSeconds(1);
        _canShowWarning = true;
    }

    void UpdateText()
    {
        float percentage = currentAmmoPercentage * 100;
        int percentageInt = Convert.ToInt32(percentage);
        _currentAmmoPercentageText.SetText(percentageInt + "%");

        if (currentAmmoPercentage > .3)
        {
            _currentAmmoPercentageText.color = _blue;
        }
        else if (currentAmmoPercentage < .3 && currentAmmoPercentage > 0)
        {
            _currentAmmoPercentageText.color = _yellow;
        }
        else
        {
            _currentAmmoPercentageText.color = _red;
        }
    }
}
