using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private AudioSource _deathAudio;
    [SerializeField] private AudioSource _takeDamageAudio;
    public SpriteRenderer spriteRenderer;
    [HideInInspector] public float spriteSize;

    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    private void Awake()
    {
        spriteSize = spriteRenderer.transform.localScale.x;
        _currentHealth = _maxHealth;
    }

    public void Splode()
    {
        //int randomInt = Random.Range(4, 8);
        for (int i = 0; i < 6; i++)
        {
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            Vector3 randomPosition = new Vector3(transform.position.x + randomX, transform.position.y + randomY);
            GameObject particle = Instantiate(_particlePrefab, randomPosition, this.transform.rotation);
            //TODO Make particle color match enemy color
            //TODO Make particle animate and destroy on animation end
            //_deathAudio.Play();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageToTake)
    {
        StartCoroutine(FlashColor());
        _takeDamageAudio.Play();
        _currentHealth -= damageToTake;
        if (_currentHealth <= 0)
        {
            Splode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerHit")
        {
            _takeDamageAudio.Play();
            V3PlayerCharacterControler temp = collision.gameObject.GetComponentInParent<V3PlayerCharacterControler>();
            float tempDamage = temp.meleeDamageValue;
            TakeDamage(tempDamage);
        }

        if (collision.gameObject.tag == "Player")
        {
            V3PlayerCharacterControler playerChar = collision.GetComponent<V3PlayerCharacterControler>();
            playerChar.ApplyDamage(5);
        }
    }

    IEnumerator FlashColor()
    {
        spriteRenderer.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = Color.white;
    }
}
