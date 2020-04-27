using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SamusLaser : MonoBehaviour
{
    [SerializeField] private EnergyBall _energyBallPrefab;
    private EnergyBall _currentBall;
    private float _potentialDamage;
    private float _timeCharged;
    public bool _shouldIncreaseTimeCharged;
    [SerializeField] private Transform _firedEnergyBallParent;
    [SerializeField] private float _maxBallSize;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private AudioSource _chargeAudio;
    [SerializeField] private AudioSource _shootAudio;
    [SerializeField] private float _shootForce;

    private void Start()
    {
        CreateNewEnergyBall();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && GunAndAmmoManager.Instance.currentAmmoPercentage > .25f)
        {
            _shouldIncreaseTimeCharged = true;
            _chargeAudio.Play();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (_timeCharged > 1)
            {
                //FireLaser();
                Shoot();
            }
            else
            {
                Destroy(_currentBall.gameObject);
                _chargeAudio.Stop();
            }

            _shouldIncreaseTimeCharged = false;
            ResetTimeCharged();
        }

        if (_shouldIncreaseTimeCharged)
        {
            IncreaseTimeCharged();
        }

        if (_timeCharged > 1)
        {
            GrowLaser();
        }
    }

    private void FireLaser()
    {
        _currentBall.rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _currentBall.transform.SetParent(_firedEnergyBallParent);
        _currentBall.rigidbody2D.AddForce(Vector2.right * 50);
        Destroy(_currentBall, 5);
        _chargeAudio.Stop();
        _shootAudio.Play();
    }

    private void GrowLaser()
    {
        if (_currentBall.transform.localScale.x < _maxBallSize)
        {
            _currentBall.transform.localScale += new Vector3(.013f, .013f, 0);
            _potentialDamage += 0.01f;
        }
    }

    private void ResetTimeCharged()
    {
        _timeCharged = 0;
        CreateNewEnergyBall();
    }

    private void IncreaseTimeCharged()
    {
        _timeCharged += Time.deltaTime;
        print(_timeCharged);
    }

    void CreateNewEnergyBall()
    {
        _currentBall = Instantiate(_energyBallPrefab, _firePoint.transform.position, transform.rotation, _firePoint.transform);
        _currentBall.transform.localScale = Vector3.zero;
    }

    void Shoot()
    {
        //Determine Direction to Shoot
        var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        var direction = worldMousePosition - transform.position;

        float xAndYSum = Mathf.Abs(direction.x) + Mathf.Abs(direction.y);
        float mouseX = direction.x / xAndYSum;
        float mouseY = direction.y / xAndYSum;
        Vector3 newMouseDirection = new Vector3(mouseX, mouseY, 0);

        //Vector3 directionToShoot = direction.normalized;

        _currentBall.rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _currentBall.transform.SetParent(_firedEnergyBallParent);
        _currentBall.rigidbody2D.AddForce(newMouseDirection * _shootForce);
        _currentBall.damage = _currentBall.transform.localScale.x * 10f;
        Destroy(_currentBall.gameObject, 5);
        _chargeAudio.Stop();
        _shootAudio.Play();

        GunAndAmmoManager.Instance.DeductAmmoPercentage(.3f);
    }
}
