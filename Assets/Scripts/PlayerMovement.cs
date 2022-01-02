using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = Vector3.zero;
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            var direction = Vector2.right * horizontal;
            var currentPosition = (Vector2)transform.position;
            _rigidbody.MovePosition(currentPosition + direction * _speed * Time.fixedDeltaTime);
        }
    }
}