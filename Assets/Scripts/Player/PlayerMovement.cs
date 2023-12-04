using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerCharacterController _controller;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;

    private Vector2 _movementDirection = Vector2.zero;

    private void Awake()
    {
        _controller = GetComponent<PlayerCharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _controller.OnMoveEvent += Move;
        _controller.OnLookEvent += OnAim;
    }

    private void FixedUpdate()
    {
        ApplyMovement(_movementDirection);
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * 5;
        _rigidbody.velocity = direction;
    }

    public void OnAim(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        _sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        if(Mathf.Abs(rotZ) > 90f)
        {
            _sprite.flipX = true;
        }
        else
            _sprite.flipX = false;

    }
}
