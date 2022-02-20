using Game.Enums;
using System;
using UnityEngine;

namespace Game.Commands.Movement
{
    [Serializable]
    public class MoveLeftCommand : WalkingCommand
    {
        protected override Vector2 MoveDirection => Vector2.left;
        public override MoveCommandType CommandType { get => MoveCommandType.MoveLeft; }

        public MoveLeftCommand(KeyCode associatedKey, float speedValue) : base(associatedKey, speedValue)
        {
        }
    }
}