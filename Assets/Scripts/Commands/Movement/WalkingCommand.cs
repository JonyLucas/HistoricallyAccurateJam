using UnityEngine;

namespace Game.Commands.Movement
{
    public abstract class WalkingCommand : BaseMoveCommand
    {
        public override string AnimationParameter { get => "isWalking"; }
        protected abstract Vector2 MoveDirection { get; }

        protected WalkingCommand(KeyCode associatedKey, float speedValue) : base(associatedKey, speedValue)
        {
        }

        protected override void ExecuteAction(GameObject gameObject)
        {
            moveScript.IsWalking = true;
            moveScript.IsFacingRight = MoveDirection == Vector2.right;
            animator.SetBool(AnimationParameter, true);
            renderer.flipX = !moveScript.IsFacingRight;

            var velocity = speed * MoveDirection;
            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }

        protected override bool ExecutionCodition(GameObject gameObject)
        {
            return !moveScript.IsCrouching && !moveScript.IsPaused;
        }

        public override void FinalizeAction(GameObject gameObject)
        {
            moveScript.IsWalking = false;
            animator.SetBool(AnimationParameter, false);

            var velocity = Vector2.zero;
            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }
    }
}