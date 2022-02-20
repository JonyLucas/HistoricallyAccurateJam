using Game.Audio;
using Game.Commands;
using Game.Commands.Movement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMovement : PlayerInput
    {
        [SerializeField]
        private SoundFx _jumpSfx;

        [SerializeField]
        private SoundFx _landingSfx;

        private Animator _animator;
        private AudioSource _audioSource;

        public bool IsJumping { get; set; } = false;
        public bool IsFacingRight { get; set; } = true;
        public bool IsWalking { get; set; } = false;
        public bool IsCrouching { get; set; } = false;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }

        protected override void InitializeCommands()
        {
            commands = new List<BaseCommand>();
            playerControl.MoveCommands.ForEach(command =>
            {
                command.InitializeFields(gameObject);
                commands.Add(command);
            });
        }

        protected override void StopCommands()
        {
            if (IsWalking)
            {
                StopCommandType<MoveRightCommand>();
            }
        }

        private void StopCommandType<T>() where T : BaseMoveCommand
        {
            var command = commands
                .FirstOrDefault(command => command.GetType() == typeof(T));

            if (command != null)
            {
                ((BaseMoveCommand)command).FinalizeAction(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Ground"))
            {
                if (IsJumping)
                {
                    _audioSource.clip = _landingSfx.GetRandomSound();
                    _audioSource.Play();
                }

                StopCommandType<JumpCommand>();
            }
        }
    }
}