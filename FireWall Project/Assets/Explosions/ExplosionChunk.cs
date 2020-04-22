using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionChunk : MonoBehaviour
{
    [HideInInspector] public Color color;

    int speed;
    float duration;
    float _x;
    float _y;

    [SerializeField] private int _intensity;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private ExplosionShrapnel _explosionParticlePrefab;

    public void Init(Color color)
    {
        this.color = color;

        _x = Random.Range(-1.0f, 1.0f);
        _y = Random.Range(-1.0f, 1.0f);
        speed = Random.Range(-_intensity, _intensity);
        _rigidbody2D.AddForce(new Vector2(_x, _y) * speed);

        for (int i = 0; i < 12; i++)
        {
            CreateParticles();
        }
    }

    void CreateParticles()
    {
        float newX = transform.position.x + Random.Range(-0.4f, 0.4f);
        float newY = transform.position.y + Random.Range(-0.4f, 0.4f);
        Vector3 newPosition = new Vector3(newX, newY, 0);
        ExplosionShrapnel explosionParticle = Instantiate(_explosionParticlePrefab, newPosition, this.transform.rotation, this.transform);
        explosionParticle.Init(this);
    }
}
