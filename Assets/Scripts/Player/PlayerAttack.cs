using Game.Audio;
using Game.Props;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField]
        private float _maxArrowCount = 4;

        [SerializeField]
        private float _fireRate = 1f;

        [SerializeField]
        private GameObject _arrowPrefab;

        [SerializeField]
        private PlayerSoundFx _soundFx;

        private AudioSource _audioSource;
        private GameObject _arrowParent;

        private List<GameObject> _playerArrows;
        private PlayerMovement _playerMovement;
        private Animator _animator;

        private float _animationDuration;
        private bool _canShoot = true;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponent<Animator>();
            _animationDuration = _animator.runtimeAnimatorController
                .animationClips
                .FirstOrDefault(x => x.name == "player_shoot").length;

            InitializeArrows();
        }

        private void InitializeArrows()
        {
            _arrowParent = new GameObject("Arrows");
            _arrowParent.transform.position = Vector3.zero;

            _playerArrows = new List<GameObject>();
            for (int i = 0; i < _maxArrowCount; i++)
            {
                var arrowObj = Instantiate(_arrowPrefab, _arrowParent.transform);
                arrowObj.SetActive(false);
                _playerArrows.Add(arrowObj);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X) && _canShoot)
            {
                StartCoroutine(FireRateCoroutine());
                ShootArrows();
            }
        }

        private void ShootArrows()
        {
            var arrow = _playerArrows.FirstOrDefault(x => !x.activeInHierarchy);
            if (arrow != null)
            {
                _animator.SetTrigger("shoot");
                StartCoroutine(ShootCoroutine(arrow));
            }
        }

        private IEnumerator FireRateCoroutine()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_fireRate);
            _canShoot = true;
        }

        private IEnumerator ShootCoroutine(GameObject arrow)
        {
            yield return new WaitForSeconds(_animationDuration);

            arrow.transform.position = transform.position;
            arrow.GetComponent<ArrowMovement>().MoveDirection = _playerMovement.IsFacingRight ? Vector2.right : Vector2.left;
            arrow.SetActive(true);

            _audioSource.clip = _soundFx.GetRandomSound();
            _audioSource.Play();
        }

        public void IncreaseArrows()
        {
            _maxArrowCount++;
            var arrowObj = Instantiate(_arrowPrefab, _arrowParent.transform);
            arrowObj.SetActive(false);
            _playerArrows.Add(arrowObj);
        }

        public void PlayerDeath()
        {
            enabled = false;
        }
    }
}