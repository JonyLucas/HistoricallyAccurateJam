using Game.Audio;
using Game.Enemy;
using Game.Player;
using System.Linq;
using UnityEngine;

namespace Game.Props
{
    public class ArrowMovement : MonoBehaviour
    {
        [SerializeField]
        private float _originalGravity = 1;

        [SerializeField]
        private float _originalSpeed = 1;

        [SerializeField]
        private SoundFx _hitSfx;

        private float _speed;
        private float _gravity = 1;
        private bool _afterShot = true;
        private bool _hitGround = false;

        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider;
        private BoxCollider2D _trigger;
        private SpriteRenderer _renderer;
        private GameObject _mainCamera;

        public Vector2 MoveDirection { get; set; } = Vector2.right;

        private void Awake()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            _renderer = GetComponent<SpriteRenderer>();

            _rigidbody = GetComponent<Rigidbody2D>();
            _trigger = GetComponents<BoxCollider2D>().FirstOrDefault(x => x.isTrigger);
            _collider = GetComponents<BoxCollider2D>().FirstOrDefault(x => !x.isTrigger);
        }

        private void OnEnable()
        {
            _speed = _originalSpeed;
            _gravity = _originalGravity;
            _afterShot = true;
            _hitGround = false;
            _trigger.enabled = true;
            _renderer.flipX = MoveDirection == Vector2.left;

            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _collider.enabled = false;
        }

        private void FixedUpdate()
        {
            var gravityMovement = Vector2.down * _gravity;
            var horizontalMovement = MoveDirection * _speed;
            transform.Translate((gravityMovement + horizontalMovement) * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Enemy") && !_hitGround)
            {
                AudioSource.PlayClipAtPoint(_hitSfx.GetRandomSound(), _mainCamera.transform.position);
                _speed = 0;
                _gravity = 0;

                _trigger.enabled = false;
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                _collider.enabled = true;

                collision.transform.GetComponent<EnemyHealth>().Damage();
            }
            if (collision.transform.CompareTag("Player") && !_afterShot)
            {
                gameObject.SetActive(false);
                collision.gameObject.GetComponent<PlayerAttack>().UpdateArrowCount();
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
                _trigger.enabled = true;
                _hitGround = true;
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;
                _collider.enabled = false;
            }
        }
    }
}