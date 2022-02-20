using Game.Enums;
using Game.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Commands.Movement
{
    [Serializable]
    public class CrouchCommand : BaseMoveCommand
    {
        private float _animationDuration;

        public override MoveCommandType CommandType { get => MoveCommandType.Crouch; }
        public override string AnimationParameter { get => "isCrouching"; }

        public CrouchCommand(KeyCode associatedKey, float speedValue) : base(associatedKey, speedValue)
        {
        }

        protected override async void ExecuteAction(GameObject gameObject)
        {
            inputScript.IsCrouching = true;
            animator.SetBool(AnimationParameter, true);
            await Task.Delay((int)(_animationDuration / 2 * 1000));

            if (gameObject != null)
            {
                rigidbody.simulated = false;
            }
        }

        protected override bool ExecutionCodition(GameObject gameObject)
        {
            return !inputScript.IsWalking && !inputScript.IsJumping && !inputScript.IsPaused;
        }

        public override void InitializeFields(GameObject gameObject)
        {
            base.InitializeFields(gameObject);
            _animationDuration = animator.GetAnimationClipDuration("player_squat");
        }

        public override void FinalizeAction(GameObject gameObject)
        {
            throw new NotImplementedException();
        }
    }
}