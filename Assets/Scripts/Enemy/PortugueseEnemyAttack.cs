using Game.Audio;
using Game.Props;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Enemy
{
    public class PortugueseEnemyAttack : BaseEnemyAttack
    {
        [SerializeField]
        private GameObject _shotPrefab;

        [SerializeField]
        private int _maxShotsCount = 3;

        private List<GameObject> _enemyShots;

        private bool _canShoot = false;

        private void Start()
        {
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

        protected override void PerformAction()
        {
            if (isVisible)
            {
                distance = transform.position - player.transform.position;
                var yDistance = Mathf.Floor(distance.y);

                if (Mathf.Abs(yDistance) < minYDistance)
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

        private IEnumerator ShootCoroutine()
        {
            while (playerAlive)
            {
                yield return new WaitForSeconds(attackRate);
                if (_canShoot && isVisible)
                {
                    var shot = _enemyShots.FirstOrDefault(x => !x.activeInHierarchy);
                    if (shot != null)
                    {
                        if (audioSource != null)
                        {
                            audioSource.clip = soundFx.GetRandomSound();
                            audioSource.Play();
                        }
                        shot.GetComponent<GunshotMovement>().MoveDirection = !isFacingLeft ? Vector2.right : Vector2.left;
                        shot.transform.position = transform.position;
                        shot.SetActive(true);
                    }
                }
            }
        }
    }
}