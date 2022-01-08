using Game.Audio;
using Game.Props;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Enemy
{
    public class PortugueseEnemyAttack : MonoBehaviour
    {
        [SerializeField]
        private GameObject _shotPrefab;

        [SerializeField]
        private float _minYDistance = 0.5f;

        [SerializeField]
        private float _fireRate = 0.5f;

        [SerializeField]
        private int _maxShotsCount = 3;

        [SerializeField]
        private SoundFx _soundFx;

        private AudioSource _audioSource;
        private List<GameObject> _enemyShots;
        private GameObject _player;
        private SpriteRenderer _renderer;
        private Vector3 _distance;

        private bool _isFacingLeft = true;
        private bool _canShoot = false;
        private bool _playerAlive = true;
        private bool _isVisible = false;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _renderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
            InitializeShots();
            StartCoroutine(ShootCoroutine());
        }

        private void InitializeShots()
        {
            _enemyShots = new List<GameObject>();
            var gunshotParent = new GameObject("Gunshots");
            gunshotParent.transform.position = Vector3.zero;

            _enemyShots = new List<GameObject>();
            for (int i = 0; i < _maxShotsCount; i++)
            {
                var shotObj = Instantiate(_shotPrefab, gunshotParent.transform);
                shotObj.SetActive(false);
                _enemyShots.Add(shotObj);
            }
        }

        private void FixedUpdate()
        {
            if (_isVisible)
            {
                _distance = transform.position - _player.transform.position;
                var yDistance = Mathf.Floor(_distance.y);

                if (Mathf.Abs(yDistance) < _minYDistance)
                {
                    FacePlayer();
                    _canShoot = true;
                }
                else
                {
                    _canShoot = false;
                }
            }
        }

        private void FacePlayer()
        {
            if (_distance.x > 0)
            {
                _isFacingLeft = true;
            }
            else
            {
                _isFacingLeft = false;
            }

            _renderer.flipX = _isFacingLeft;
        }

        private IEnumerator ShootCoroutine()
        {
            while (_playerAlive)
            {
                yield return new WaitForSeconds(_fireRate);
                if (_canShoot && _isVisible)
                {
                    var shot = _enemyShots.FirstOrDefault(x => !x.activeInHierarchy);
                    if (shot != null)
                    {
                        if (_audioSource != null)
                        {
                            _audioSource.clip = _soundFx.GetRandomSound();
                            _audioSource.Play();
                        }
                        shot.GetComponent<GunshotMovement>().MoveDirection = !_isFacingLeft ? Vector2.right : Vector2.left;
                        shot.transform.position = transform.position;
                        shot.SetActive(true);
                    }
                }
            }
        }

        private void OnBecameInvisible()
        {
            _isVisible = false;
        }

        private void OnBecameVisible()
        {
            _isVisible = true;
        }

        public void PlayerDeath()
        {
            _playerAlive = false;
        }
    }
}