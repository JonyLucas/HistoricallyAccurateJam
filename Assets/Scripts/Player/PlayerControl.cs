using Game.Commands.Factories;
using Game.Commands.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Player Control", fileName = "Player Control", order = 57)]
    public class PlayerControl : ScriptableObject
    {
        [SerializeField]
        private List<MoveCommandFactory> _commands;

        private List<BaseMoveCommand> _moveCommands;

        public List<BaseMoveCommand> MoveCommands
        { get { return _moveCommands; } }

        private void OnEnable()
        {
            _moveCommands = new List<BaseMoveCommand>();
            _commands.ForEach(command => _moveCommands.Add(command.Create()));
        }

        public void InitializeCommands(GameObject gameObject)
        {
            _moveCommands.ForEach(command => command.InitializeFields(gameObject));
        }
    }
}