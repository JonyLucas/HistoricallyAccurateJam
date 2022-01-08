using Game.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1;

        [SerializeField]
        private float _jumpForce = 350;

        [SerializeField]
        private SoundFx _jumpSfx;

        [SerializeField]
        private SoundFx _landingSfx;

        private Rigidbody2D _rigidbody;
        private SpriteRenderer _renderer;
        private Animator _animator;
        private AudioSource _audioSource;

        private bool _canJump = true;
        private bool _isFacingRight = true;
        private bool _isMoving = false;
        private bool _isCrouching = false;

        private float _moveDirection;

        public bool IsFacingRight
        { get { return _isFacingRight; } }

        public bool IsMoving
        { get { return _isMoving; } }

        public bool IsCrouching
        { get { return _isCrouching; } }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canJump)
            {
                _audioSource.clip = _jumpSfx.GetRandomSound();
                _audioSource.Play();
                _canJump = false;
                _animator.SetBool("isJumping", true);
                _rigidbody.AddForce(Vector2.up * _jumpForce);
            }
        }

        private void FixedUpdate()
        {
            // Movements
            _moveDirection = Input.GetAxisRaw("Horizontal");
            if (_moveDirection != 0 && !_isCrouching)
            {
                Movement();
            }
            else
            {
                StopMovement();
            }

            _moveDirection = Input.GetAxisRaw("Vertical");
            if (_moveDirection < 0 && !_isMoving && _canJump)
            {
                Crouching();
            }
            else
            {
                _isCrouching = false;
                _animator.SetBool("isCrouching", _isCrouching);
                _rigidbody.simulated = true;
            }
        }

        public void StopMovement()
        {
            _isMoving = false;
            _animator.SetBool("isWalking", _isMoving);
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }

        private void Movement()
        {
            _isMoving = true;
            _rigidbody.velocity = new Vector2(_moveDirection * _speed, _rigidbody.velocity.y);
            _animator.SetBool("isWalking", _isMoving);

            // Animations
            if (_moveDirection > 0 && !_isFacingRight || _moveDirection < 0 && _isFacingRight)
            {
                _renderer.flipX = _isFacingRight;
                _isFacingRight = !_isFacingRight;
            }
        }

        private void Crouching()
        {
            StopMovement();
            _isCrouching = true;
            _animator.SetBool("isCrouching", _isCrouching);
            _rigidbody.simulated = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Ground"))
            {
                if (!_canJump)
                {
                    _audioSource.clip = _landingSfx.GetRandomSound();
                    _audioSource.Play();
                }
                _canJump = true;
                _animator.SetBool("isJumping", false);
            }
        }

        public void PlayerDeath()
        {
            StopMovement();
            enabled = false;
        }
    }
}