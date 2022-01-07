using Game.Enemy;
using UnityEngine;

namespace Game.Props
{
    public class ArrowMovement : MonoBehaviour
    {
        [SerializeField]
        private float _originalGravity = 1;

        [SerializeField]
        private float _originalSpeed = 1;

        private float _speed;
        private float _gravity = 1;

        private bool _afterShot = true;

        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider;
        private BoxCollider2D _trigger;
        private SpriteRenderer _renderer;

        public Vector2 MoveDirection { get; set; } = Vector2.right;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _trigger = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            _speed = _originalSpeed;
            _gravity = _originalGravity;
            _afterShot = true;
            _renderer.flipX = MoveDirection == Vector2.left;
        }

        private void OnDisable()
        {
            Destroy(_rigidbody);
            Destroy(_collider);
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

                _rigidbody = gameObject.AddComponent<Rigidbody2D>();
                _rigidbody.freezeRotation = true;

                _collider = gameObject.AddComponent<BoxCollider2D>();
                _collider.offset = _trigger.offset;
                _collider.size = _trigger.size;

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
                Destroy(_rigidbody);
                Destroy(_collider);
            }
        }
    }
}