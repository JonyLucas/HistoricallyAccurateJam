using Game.Enums;
using UnityEngine;

namespace Game.Commands.Movement
{
    public class JumpCommand : BaseMoveCommand
    {
        public override MoveCommandType CommandType { get => MoveCommandType.Jump; }
        public override string AnimationParameter { get => "isJumping"; }

        private bool _isVariableJump = false;
        private Vector2 _jumpForce;

        public JumpCommand(KeyCode associatedKey, float speedValue) : base(associatedKey, speedValue)
        {
        }

        public override void InitializeFields(GameObject gameObject)
        {
            base.InitializeFields(gameObject);
            _jumpForce = rigidbody.gravityScale * rigidbody.mass * -Physics2D.gravity;
        }

        protected override void ExecuteAction(GameObject gameObject)
        {
            moveScript.IsJumping = true;
            moveScript.IsOnGround = false;
            animator.SetBool(AnimationParameter, true);

            rigidbody.AddForce(_jumpForce * speed);
        }

        public override void FinalizeAction(GameObject gameObject)
        {
            if (!moveScript.IsOnGround && !_isVariableJump)
            {
                var velocity = rigidbody.velocity;
                if (velocity.y > 0)
                {
                    velocity.y /= 2;
                    rigidbody.velocity = velocity;
                    _isVariableJump = true;
                }
            }
            else if (moveScript.IsOnGround)
            {
                animator.SetBool(AnimationParameter, false);
                _isVariableJump = false;
            }
        }

        protected override bool ExecutionCodition(GameObject gameObject)
        {
            return !moveScript.IsJumping && moveScript.IsOnGround;
        }
    }
}