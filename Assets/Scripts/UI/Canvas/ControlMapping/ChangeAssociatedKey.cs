using Game.Commands.Factories;
using Game.Commands.Movement;
using Game.Enums;
using Game.Player.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ChangeAssociatedKey : MonoBehaviour
    {
        [SerializeField]
        private PlayerControl _playerControl;

        [SerializeField]
        private Text _leftCommandText;

        [SerializeField]
        private Text _rightCommandText;

        [SerializeField]
        private Text _jumpCommandText;

        [SerializeField]
        private Text _crouchCommandText;

        private BaseMoveCommand _selectedMoveCommand;

        private List<MoveCommandFactory> _commandsFactory;

        private void Awake()
        {
            UpdateButtonText();
            InitializeFactories();
        }

        public void UpdateButtonText()
        {
            _leftCommandText.text = _playerControl.MoveCommands
                .Find(command => command.CommandType == MoveCommandType.MoveLeft)
                .AssociatedKey.ToString();

            _rightCommandText.text = _playerControl.MoveCommands
                .Find(command => command.CommandType == MoveCommandType.MoveRight)
                .AssociatedKey.ToString();

            _crouchCommandText.text = _playerControl.MoveCommands
                .Find(command => command.CommandType == MoveCommandType.Crouch)
                .AssociatedKey.ToString();

            _jumpCommandText.text = _playerControl.MoveCommands
                .Find(command => command.CommandType == MoveCommandType.Jump)
                .AssociatedKey.ToString();
        }

        private void InitializeFactories()
        {
            _commandsFactory = new List<MoveCommandFactory>();
            _commandsFactory.Add(new MoveCommandFactory { CommandType = MoveCommandType.MoveRight });
            _commandsFactory.Add(new MoveCommandFactory { CommandType = MoveCommandType.MoveLeft });
            _commandsFactory.Add(new MoveCommandFactory { CommandType = MoveCommandType.Crouch });
            _commandsFactory.Add(new MoveCommandFactory { CommandType = MoveCommandType.Jump });
        }

        public void SetCommand(int moveCommandType)
        {
            _selectedMoveCommand = _playerControl.MoveCommands.FirstOrDefault(command => command.CommandType == (MoveCommandType)moveCommandType);
        }

        private void OnGUI()
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if (e.keyCode != KeyCode.Escape && _selectedMoveCommand != null)
                {
                    var factory = _commandsFactory.Find(factory => factory.CommandType == _selectedMoveCommand.CommandType);
                    if (factory != null)
                    {
                        factory.AssociatedKey = e.keyCode;
                        factory.Speed = _selectedMoveCommand.Speed;
                        _playerControl.MoveCommands.Add(factory.Create());
                        _playerControl.MoveCommands.Remove(_selectedMoveCommand);
                    }
                }

                UpdateButtonText();
                gameObject.SetActive(false);
            }
        }
    }
}