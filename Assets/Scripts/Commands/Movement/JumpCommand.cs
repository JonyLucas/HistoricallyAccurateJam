using Game.Enums;
using UnityEngine;

namespace Game.Commands.Movement
{
    public class JumpCommand : BaseMoveCommand
    {
        public override MoveCommandType CommandType { get => MoveCommandType.Jump; }
        public override string AnimationParameter { get => "isJumping"; }

        public JumpCommand(KeyCode associatedKey, float speedValue) : base(associatedKey, speedValue)
        {
        }

        protected override void ExecuteAction(GameObject gameObject)
        {
            moveScript.IsJumping = true;
            animator.SetBool(AnimationParameter, true);
            var forceScale = -Physics2D.gravity * rigidbody.gravityScale * rigidbody.mass;
            Debug.Log(forceScale);
            rigidbody.AddForce(Vector2.up * 500);
        }

        public override void FinalizeAction(GameObject gameObject)
        {
            moveScript.IsJumping = false;
            animator.SetBool(AnimationParameter, false);
        }

        protected override bool ExecutionCodition(GameObject gameObject)
        {
            return !moveScript.IsJumping;
        }
    }
}