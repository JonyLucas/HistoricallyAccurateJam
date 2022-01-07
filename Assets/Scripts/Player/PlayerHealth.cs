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
        private int _lives = 4;

        [SerializeField]
        private int _maxLives = 4;

        [SerializeField]
        private IntGameEvent _event;

        [SerializeField]
        private GameEvent _playerDeathEvent;

        private Animator _animator;

        private void Awake()
        {
            _lives = _maxLives;
            _animator = GetComponent<Animator>();
        }

        public void Damage()
        {
            _lives--;

            if (_lives <= 0)
            {
                if (_lives < 0)
                {
                    _lives = 0;
                }

                _animator.SetTrigger("death");
                _playerDeathEvent.OnOcurred();
            }

            _event.OnOcurred(_lives);
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
    }
}