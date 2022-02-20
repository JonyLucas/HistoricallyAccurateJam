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
            inputScript.IsWalking = true;
            animator.SetBool(AnimationParameter, true);
            renderer.flipX = MoveDirection == Vector2.left;

            var velocity = speed * MoveDirection;
            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }

        protected override bool ExecutionCodition(GameObject gameObject)
        {
            return !inputScript.IsCrouching && !inputScript.IsPaused;
        }

        public override void FinalizeAction(GameObject gameObject)
        {
            inputScript.IsWalking = false;
            animator.SetBool(AnimationParameter, false);

            var velocity = Vector2.zero;
            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }
    }
}