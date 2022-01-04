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

    private bool _canJump = true;
    private bool _facingRight = true;

    private float _moveDirection;

    public bool FacingRight { get { return _facingRight; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //transform.position = Vector3.zero;
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
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }
    }

    private void Movement()
    {
        _rigidbody.velocity = new Vector2(_moveDirection * _speed, _rigidbody.velocity.y);

        // Animations
        if (_moveDirection > 0 && !_facingRight || _moveDirection < 0 && _facingRight)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        _renderer.flipX = _facingRight;
        _facingRight = !_facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _canJump = true;
        }
    }
}