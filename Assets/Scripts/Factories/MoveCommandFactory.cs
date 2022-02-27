using Game.Commands.Movement;
using Game.Enums;
using System;
using UnityEngine;

namespace Game.Commands.Factories
{
    [Serializable]
    public class MoveCommandFactory
    {
        [SerializeField]
        private KeyCode _associatedKey;

        [SerializeField]
        private MoveCommandType _commandType;

        [SerializeField]
        private float _moveSpeed = 1;

        public KeyCode AssociatedKey
        {
            get { return _associatedKey; }
            set { _associatedKey = value; }
        }

        public MoveCommandType CommandType
        {
            get { return _commandType; }
            set { _commandType = value; }
        }

        public float Speed
        {
            get { return _moveSpeed; }
            set { _moveSpeed = value; }
        }

        public BaseMoveCommand Create()
        {
            switch (_commandType)
            {
                case MoveCommandType.MoveLeft:
                    return new MoveLeftCommand(_associatedKey, _moveSpeed);

                case MoveCommandType.MoveRight:
                    return new MoveRightCommand(_associatedKey, _moveSpeed);

                case MoveCommandType.Crouch:
                    return new CrouchCommand(_associatedKey, _moveSpeed);

                case MoveCommandType.Jump:
                    return new JumpCommand(_associatedKey, _moveSpeed);

                default:
                    return null;
            }
        }
    }
}