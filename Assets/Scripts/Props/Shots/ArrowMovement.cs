using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField]
    private float _originalSpeed = 1;

    private float _speed;

    [SerializeField]
    private float _gravity = 1;

    private Rigidbody2D _rb;
    private Collider2D _collider;
    
    public Vector2 MoveDirection { get; set; } = Vector2.right;

    private bool _afterShot = true;

    private void OnEnable()
    {
        _speed = _originalSpeed;
        _afterShot = true;
    }

    private void FixedUpdate()
    {
        var gravityMovement = Vector2.down * _gravity;
        var horizontalMovement = MoveDirection * _speed;
        transform.Translate((gravityMovement + horizontalMovement) * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            _speed = 0;
            _gravity = 0;
            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.freezeRotation = true;
            _collider = gameObject.AddComponent<BoxCollider2D>();
            collision.transform.GetComponent<EnemyHealth>().Damage();
        }
        if (collision.transform.CompareTag("Player") && !_afterShot)
        {
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            _afterShot = false;
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Destroy(_rb);
            Destroy(_collider);
        }
    }

}