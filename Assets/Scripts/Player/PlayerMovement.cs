using Game.Audio;
using Game.Commands.Movement;
using Game.Player.ScriptableObjects;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private PlayerControl _control;

        private float _jumpForce = 10;
        private float _gravity = 0.2f;

        [SerializeField]
        private SoundFx _jumpSfx;

        [SerializeField]
        private SoundFx _landingSfx;

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private AudioSource _audioSource;

        public bool IsJumping { get; set; } = false;
        public bool IsFacingRight { get; set; } = true;
        public bool IsMoving { get; set; } = false;
        public bool IsCrouching { get; set; } = false;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.anyKey)
            {
                var command = _control.MoveCommands
                    .FirstOrDefault(command => Input.GetKey(command.AssociatedKey));

                if (command != null)
                {
                    HorizontalMovement(command);
                    CrouchMovement(command);
                }
            }
            else
            {
                if (IsMoving)
                {
                    StopMovement();
                }

                if (IsCrouching)
                {
                    StopCrouching();
                }
            }

            bool upMovement = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
            if (upMovement && !IsJumping)
            {
                _audioSource.clip = _jumpSfx.GetRandomSound();
                _audioSource.Play();
                IsJumping = true;
                JumpMovement(null);
                _animator.SetBool("isJumping", true);
                //_rigidbody.AddForce(Vector2.up * _jumpForce);
            }
        }

        private void HorizontalMovement(BaseMoveCommand command)
        {
            if (command.GetType() == typeof(MoveRightCommand))
            {
                command.Execute(gameObject);
                IsFacingRight = true;
                IsMoving = true;
            }
            else if (command.GetType() == typeof(MoveLeftCommand))
            {
                command.Execute(gameObject);
                IsFacingRight = false;
                IsMoving = true;
            }
            else
            {
                StopMovement();
            }
        }

        private void JumpMovement(BaseMoveCommand command)
        {
            Debug.Log("JUMP");
            StartCoroutine(JumpCoroutine());
        }

        private IEnumerator JumpCoroutine()
        {
            var currentSpeed = _jumpForce;
            transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);
            while (IsJumping)
            {
                yield return null;
                transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);
                currentSpeed -= _gravity;
            }
        }

        private void CrouchMovement(BaseMoveCommand command)
        {
            if (command.GetType() == typeof(CrouchCommand))
            {
                command.Execute(gameObject);
                IsCrouching = true;
            }
        }

        public void StopMovement()
        {
            IsMoving = false;
            _animator.SetBool("isWalking", IsMoving);
        }

        private void StopCrouching()
        {
            IsCrouching = false;
            _animator.SetBool("isCrouching", IsCrouching);
            _rigidbody.simulated = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Ground"))
            {
                if (IsJumping)
                {
                    _audioSource.clip = _landingSfx.GetRandomSound();
                    _audioSource.Play();
                }
                Debug.Log("END OF JUMP");
                IsJumping = false;
                _animator.SetBool("isJumping", IsJumping);
            }
        }

        public void PlayerDeath()
        {
            StopMovement();
            enabled = false;
        }
    }
}