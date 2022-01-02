using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    private Rigidbody2D _rigidbody;

    private bool facingRight = true;
    private float _moveDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Movements
        _moveDirection = Input.GetAxis("Horizontal");
        if (_moveDirection != 0)
        {
            var direction = Vector2.right * _moveDirection;
            var currentPosition = (Vector2)transform.position;
            _rigidbody.MovePosition(currentPosition + direction * _speed * Time.fixedDeltaTime);
        }

        // Animations
        if (_moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (_moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}