using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    private float _jumpForce = 350;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private Animator _animator;

    private bool _canJump = true;
    private bool _isFacingRight = true;
    private bool _isMoving = false;

    private float _moveDirection;

    public bool IsFacingRight
    { get { return _isFacingRight; } }

    public bool IsMoving
    { get { return _isMoving; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            _canJump = false;
            _rigidbody.AddForce(Vector2.up * _jumpForce);
        }
    }

    private void FixedUpdate()
    {
        // Movements
        _moveDirection = Input.GetAxisRaw("Horizontal");
        if (_moveDirection != 0)
        {
            Movement();
        }
        else
        {
            _isMoving = false;
            _animator.SetBool("isWalking", _isMoving);
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }
    }

    private void Movement()
    {
        _isMoving = true;
        _rigidbody.velocity = new Vector2(_moveDirection * _speed, _rigidbody.velocity.y);
        _animator.SetBool("isWalking", _isMoving);

        // Animations
        if (_moveDirection > 0 && !_isFacingRight || _moveDirection < 0 && _isFacingRight)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        _renderer.flipX = _isFacingRight;
        _isFacingRight = !_isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _canJump = true;
        }
    }
}