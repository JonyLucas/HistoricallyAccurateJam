using Game.Commands.Movement;
using Game.Player.ScriptableObjects;
using Game.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Player
{
    public class PlayerInput : MonoBehaviour
    {
        // Fields

        [SerializeField]
        private PlayerControl _control;

        private List<BaseMoveCommand> _moveCommands;

        // Properties

        public bool IsWalking { get; set; }
        public bool IsJumping { get; set; }
        public bool IsCrouching { get; set; }
        public bool IsPaused { get; set; }

        private void Start()
        {
            _moveCommands = _control.MoveCommands;
            _moveCommands.ForEach(command => command.InitializeFields(gameObject));

            PauseMenu.OpenWindowEvent += PauseMovement;
        }

        private void FixedUpdate()
        {
            if (Input.anyKey && !IsPaused)
            {
                var command = _moveCommands.FirstOrDefault(command => Input.GetKey(command.AssociatedKey));

                if (command != null)
                {
                    command.Execute(gameObject);
                }
            }
            else
            {
                if (IsWalking)
                {
                    StopMovement();
                }
            }
        }

        public void StopMovement()
        {
            var command = _moveCommands
            .FirstOrDefault(command => command.GetType().BaseType == typeof(WalkingCommand));

            if (command != null)
            {
                command.FinalizeAction(gameObject);
            }
        }

        public void PlayerDeath()
        {
            StopMovement();
            enabled = false;
        }

        private void PauseMovement(bool isPaused)
        {
            IsPaused = isPaused;
        }

        private void OnDisable()
        {
            PauseMenu.OpenWindowEvent -= PauseMovement;
        }
    }
}