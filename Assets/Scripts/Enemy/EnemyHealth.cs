using Game.Audio;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField]
        private SoundFx _deathSfx;

        [SerializeField]
        private int _maxLives = 1;

        private int _lives;

        private GameObject _mainCamera;
        private SpriteRenderer _renderer;

        private void Start()
        {
            _lives = _maxLives;
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void Damage()
        {
            _lives--;

            _renderer.color = new Color((255f * _lives) / _maxLives, 0, 0);

            if (_lives <= 0)
            {
                AudioSource.PlayClipAtPoint(_deathSfx.GetRandomSound(), _mainCamera.transform.position);
                Destroy(gameObject);
            }
        }
    }
}