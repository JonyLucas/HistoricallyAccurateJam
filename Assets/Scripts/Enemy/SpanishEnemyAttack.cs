using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class SpanishEnemyAttack : BaseEnemyAttack
    {
        [SerializeField]
        private float _speed;

        private Vector2 _moveDirection;

        private PlayerMovement _playerMoveScript;

        private void Start()
        {
            _playerMoveScript = player.GetComponent<PlayerMovement>();
        }

        protected override void PerformAction()
        {
            distance = transform.position - player.transform.position;
            var yDistance = Mathf.Floor(distance.y);

            if (Mathf.Abs(yDistance) < minYDistance && !_playerMoveScript.IsCrouching)
            {
                FacePlayer();
                PorsuitPlayer();
            }
            else
            {
                animator.SetBool("isAttacking", false);
            }
        }

        private void PorsuitPlayer()
        {
            _moveDirection = isFacingLeft ? Vector2.left : Vector2.right;
            transform.Translate(_moveDirection * _speed * Time.fixedDeltaTime);
            animator.SetBool("isAttacking", true);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                audioSource.clip = soundFx.GetRandomSound();
                audioSource.Play();
                collision.gameObject.GetComponent<PlayerHealth>().Damage();
            }
        }
    }
}