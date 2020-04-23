using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    [SerializeField] private GameObject _groundSpikePrefab;
    [SerializeField] private float _distanceBetweenSpikes;
    [SerializeField] private int _amountOfSpikesToSpawn;
    [SerializeField] private float _secondsBetweenSpawns;
    private Vector3 _spikePosition;
    private Vector3 _target;

    public void Init(Vector3 target)
    {
        _target = target;
        StartCoroutine(nameof(SpawnSpikes));
    }

    private IEnumerator SpawnSpikes()
    {
        _spikePosition = this.transform.position;

        int directionModifier = 1;
        if (_target.x < transform.position.x)
        {
            directionModifier = -1;
        }

        //print("_target.x: " + _target.x);
        //print("transform.position.x: " + transform.position.x);
        //print("directionModifier: " + directionModifier);

        for (int i = 0; i < _amountOfSpikesToSpawn; i++)
        {
            _spikePosition = new Vector3(_spikePosition.x + (_distanceBetweenSpikes * directionModifier), _spikePosition.y);
            GameObject groundSpike = Instantiate(_groundSpikePrefab, _spikePosition, this.transform.rotation, this.transform);
            yield return new WaitForSeconds(_secondsBetweenSpawns);
        }

        Destroy(gameObject, 2);
    }
}
