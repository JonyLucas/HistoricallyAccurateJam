using Game.Commands;
using Game.Commands.Movement;
using Game.Player.ScriptableObjects;
using Game.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Player
{
    public abstract class PlayerInput : MonoBehaviour
    {
        // Fields

        [SerializeField]
        protected PlayerControl playerControl;

        protected List<BaseCommand> commands;

        // Properties

        public bool IsPaused { get; set; }

        private void FixedUpdate()
        {
            if (Input.anyKey && !IsPaused)
            {
                var executionCommands = commands.Where(command => Input.GetKey(command.AssociatedKey));

                if (executionCommands.Any())
                {
                    foreach (var command in executionCommands)
                    {
                        command.Execute(gameObject);
                    }
                }
            }
            else
            {
                StopCommands();
            }
        }

        private void Start()
        {
            InitializeCommands();
            PauseMenu.OpenWindowEvent += PauseMovement;
        }

        protected abstract void InitializeCommands();

        protected abstract void StopCommands();

        public void PlayerDeath()
        {
            StopCommands();
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