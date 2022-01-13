using System;
using UnityEngine;

namespace Game.Commands.Movement
{
    [Serializable]
    public class CrouchCommand : BaseMoveCommand
    {
        public CrouchCommand(KeyCode associatedKey, float speedValue) : base(associatedKey, speedValue)
        {
        }

        public override string AnimationParameter { get => "isCrouching"; }

        protected override void ExecuteAction(GameObject gameObject)
        {
            animator.SetBool(AnimationParameter, true);
        }

        protected override bool ExecutionCodition(GameObject gameObject)
        {
            return !moveScript.IsCrouching && !moveScript.IsJumping;
        }
    }
}