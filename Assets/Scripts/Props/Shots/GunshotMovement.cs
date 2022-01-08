using Game.Audio;
using Game.Player;
using UnityEngine;

namespace Game.Props
{
    public class GunshotMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 10;

        [SerializeField]
        private SoundFx _hitSfx;

        private GameObject _mainCamera;
        public Vector2 MoveDirection { get; set; } = Vector2.left;

        private void Awake()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        private void FixedUpdate()
        {
            transform.Translate(MoveDirection * _speed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(_hitSfx.GetRandomSound(), _mainCamera.transform.position);
                collision.gameObject.GetComponent<PlayerHealth>().Damage();
                gameObject.SetActive(false);
            }
        }

        private void OnBecameInvisible()
        {
            gameObject.SetActive(false);
        }
    }
}