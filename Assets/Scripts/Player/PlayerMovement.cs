using Game.Audio;
using Game.Commands;
using Game.Commands.Movement;
using System.Collections;
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

        private AudioSource _audioSource;

        public bool IsJumping { get; set; }
        public bool IsOnGround { get; set; } = true;
        public bool IsFacingRight { get; set; } = true;
        public bool IsWalking { get; set; }
        public bool IsCrouching { get; set; }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
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
            if (IsJumping)
            {
                IsJumping = false;
                StopCommandType<JumpCommand>();
            }
            if (IsCrouching)
            {
                StopCommandType<CrouchCommand>();
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
                StartCoroutine(StopJumpCoroutine());
            }
        }

        private IEnumerator StopJumpCoroutine()
        {
            IsOnGround = true;
            StopCommandType<JumpCommand>();
            yield return new WaitForSeconds(1f);
            IsJumping = false;
        }
    }
}