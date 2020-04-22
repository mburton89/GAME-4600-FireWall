using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HectorA : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private float _secondsBetweenAttacks;
    [SerializeField] private GroundPound _groundPoundPrefab;
    [SerializeField] private ChargeAttack _chargeAttack;
    [SerializeField] private float _sightMinimum;
    [SerializeField] private float _sightMaximum;
    private Transform _player;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _idleSprites;
    [SerializeField] private List<Sprite> _chargeSprites;
    [SerializeField] private List<Sprite> _stompSprites;
    [SerializeField] private List<Sprite> _shankSprites;
    [SerializeField] private List<Sprite> _throwSprites;

    [SerializeField] private float _secondsBetweenIdleFrames;
    [SerializeField] private float _secondsBetweenChargeFrames;
    [SerializeField] private float _secondsBetweenStompFrames;
    [SerializeField] private float _secondsBetweenShankFrames;
    [SerializeField] private float _secondsBetweenThrowFrames;

    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    [SerializeField] private GameObject _trojanSplosion;
    private HectorATakeDamage _takeDamage;

    [SerializeField] private AudioSource _takeDamageAudio;
    [SerializeField] private AudioSource _gallopAudio;
    [SerializeField] private AudioSource _stompAudio;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(nameof(InitRandomAttack));
        InvokeRepeating("CheckPlayerDirection", 0, 1);
        _currentHealth = _maxHealth;
        _takeDamage = GetComponentInChildren<HectorATakeDamage>();
        _takeDamage.Init(this);
    }

    void StompGround()
    {
        StartCoroutine(nameof(StompCo));
    }

    void ThrowSpearAtPlayer()
    {

    }

    void ChargePlayer()
    {
       // _chargeAttack.Dash(_player.transform.position);
        StartCoroutine(nameof(ChargeCo));
    }

    private IEnumerator InitRandomAttack()
    {
        float distance = Vector3.Distance(_player.position, transform.position);
        if (distance < _sightMaximum)
        {
            int scenario = Random.Range(0, 2);
            if (scenario == 0)
            {
                ChargePlayer();
            }
            else
            {
                StompGround();
            }
        }

        yield return new WaitForSeconds(_secondsBetweenAttacks);
        StartCoroutine(nameof(InitRandomAttack));
    }

    private IEnumerator StompCo()
    {
        Vector3 playerPosition = _player.transform.position;
        _stompAudio.Play();
        for (int i = 0; i < _stompSprites.Count; i++)
        {
            _spriteRenderer.sprite = _stompSprites[i];
            yield return new WaitForSeconds(_secondsBetweenStompFrames);
        }
        CinemachineCameraShaker.Instance.ShakeCamera(0.5f);
        GroundPound groundPound = Instantiate(_groundPoundPrefab, new Vector3(transform.position.x, transform.position.y - 4.92f), transform.rotation);
        groundPound.Init(playerPosition);
        //GetComponent<Rigidbody2D>().AddForce(Vector2.up * 900);
        //yield return new WaitForSeconds(.75f);
        ShowIdle();
    }

    private IEnumerator ChargeCo()
    {
        _chargeAttack.StartDash(_player.transform.position);
        _gallopAudio.Play();
        for (int i = 0; i < _chargeSprites.Count; i++)
        {
            _spriteRenderer.sprite = _chargeSprites[i];
            yield return new WaitForSeconds(_secondsBetweenChargeFrames);
        }
        _gallopAudio.Stop();
        _chargeAttack.EndDash();
        ShowIdle();


    }

    void CheckPlayerDirection()
    {
        if (_player.position.x < transform.position.x - _sightMinimum)
        {
            _spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_player.position.x > transform.position.x + _sightMinimum)
        {
            _spriteRenderer.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void ShowIdle()
    {
        _spriteRenderer.sprite = _idleSprites[0];
    }

    private IEnumerator IdleCo()
    {
        for (int i = 0; i < _idleSprites.Count; i++)
        {
            _spriteRenderer.sprite = _idleSprites[i];
            yield return new WaitForSeconds(_secondsBetweenIdleFrames);
        }
    }

    public void ApplyDamage(float damage)
    {
        //Actual decrement of health. Can be changed as development continues.
        _takeDamageAudio.Play();
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            GameObject splosion = Instantiate(_trojanSplosion, new Vector3(transform.position.x, transform.position.y + 0.5f), transform.rotation);
            splosion.transform.localScale = new Vector3(3, 3, 0);
            Destroy(this.gameObject);
        }
    }
}
