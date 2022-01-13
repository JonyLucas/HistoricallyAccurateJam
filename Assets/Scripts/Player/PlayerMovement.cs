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

        private bool _canJump = true;
        private bool _isFacingRight = true;
        private bool _isMoving = false;
        private bool _isCrouching = false;

        public bool IsJumping
        { get { return !_canJump; } }

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
            _animator = GetComponent<Animator>();
            _control.InitializeCommands(gameObject);
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
            if (upMovement && _canJump)
            {
                _audioSource.clip = _jumpSfx.GetRandomSound();
                _audioSource.Play();
                _canJump = false;
                JumpMovement(null);
                _animator.SetBool("isJumping", true);
                //_rigidbody.AddForce(Vector2.up * _jumpForce);
            }
        }

        // Check later
        private void OnGUI()
        {
            Event e = Event.current;
            if (e.isKey)
            {
                Debug.Log("Detected Key: " + e.keyCode);
            }
        }

        private void HorizontalMovement(BaseMoveCommand command)
        {
            if (command.GetType() == typeof(MoveRightCommand))
            {
                command.Execute(gameObject);
                _isFacingRight = true;
                _isMoving = true;
            }
            else if (command.GetType() == typeof(MoveLeftCommand))
            {
                command.Execute(gameObject);
                _isFacingRight = false;
                _isMoving = true;
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
                _isCrouching = true;
            }
        }

        public void StopMovement()
        {
            _isMoving = false;
            _animator.SetBool("isWalking", _isMoving);
        }

        private void StopCrouching()
        {
            _isCrouching = false;
            _animator.SetBool("isCrouching", _isCrouching);
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
                _canJump = true;
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