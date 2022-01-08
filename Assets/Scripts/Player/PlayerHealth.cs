using Game.Audio;
using Game.Enums;
using Game.ScriptableObjects.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private SoundFx _sfx;

        [SerializeField]
        private int _lives = 4;

        [SerializeField]
        private int _maxLives = 4;

        [SerializeField]
        private IntGameEvent _event;

        [SerializeField]
        private GameEvent _playerDeathEvent;

        private Animator _animator;
        private AudioSource _audioSource;
        private SpriteRenderer _renderer;
        private float _hitFeedbackDuration = 0.3f;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _lives = _maxLives;
            _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void Damage()
        {
            _lives--;

            StartCoroutine(HitFeedback());

            if (_lives <= 0)
            {
                if (_lives < 0)
                {
                    _lives = 0;
                }
                _audioSource.clip = _sfx.GetRandomSound();
                _audioSource.Play();
                _animator.SetTrigger("death");
                _playerDeathEvent.OnOcurred();
            }

            _event.OnOcurred(_lives);
        }

        private IEnumerator HitFeedback()
        {
            _renderer.color = new Color(150, 0, 0);
            yield return new WaitForSeconds(_hitFeedbackDuration);
            _renderer.color = Color.white;
        }

        public void Heal(CollectablesType collectablesType)
        {
            if (_lives < _maxLives)
            {
                if (collectablesType == CollectablesType.Pineapple)
                {
                    _lives = _maxLives;
                }
                else if (collectablesType == CollectablesType.Banana)
                {
                    _lives++;
                }
            }

            _event.OnOcurred(_lives);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Spike"))
            {
                _lives = 0;
                Damage();
            }
        }
    }
}