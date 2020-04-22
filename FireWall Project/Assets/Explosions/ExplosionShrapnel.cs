using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionShrapnel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    int speed;
    float _x;
    float _y;

    [SerializeField] private int _intensity;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public void Init(ExplosionChunk _controller)
    {
        //Sets Random Color
        //_spriteRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        _spriteRenderer.color = _controller.color;
        _x = Random.Range(-1.0f, 1.0f);
        _y = Random.Range(-1.0f, 1.0f);
        speed = Random.Range(_intensity, _intensity * 2);
        _rigidbody2D.AddForce(new Vector2(_x, _y) * speed);
    }

    private void Update()
    {
        if (_spriteRenderer.color.a > 0)
        {
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a - 0.01f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
