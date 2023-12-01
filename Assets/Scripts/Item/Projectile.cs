using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    [SerializeField] float _speed = 10.0f;
    [SerializeField] float _lifeTime;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D == null) _rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0.0f;
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    public void SetForward(Vector2 forward)
    {
        _rigidbody2D.velocity = forward * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
