using System;
using UnityEngine;

namespace Game.Commands.Movement
{
    [Serializable]
    public class MoveRightCommand : BaseMoveCommand
    {
        public override string AnimationParameter { get => "isWalking"; }

        public MoveRightCommand(KeyCode associatedKey, float speedValue) : base(associatedKey, speedValue)
        {
        }

        protected override void ExecuteAction(GameObject gameObject)
        {
            gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
            animator.SetBool(AnimationParameter, true);
            renderer.flipX = false;
        }

        protected override bool ExecutionCodition(GameObject gameObject)
        {
            return !moveScript.IsCrouching && !moveScript.IsJumping;
        }
    }
}