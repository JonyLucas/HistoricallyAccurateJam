using Game.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Commands.Movement
{
    [Serializable]
    public class CrouchCommand : BaseMoveCommand
    {
        private Rigidbody2D _rigidBody;
        private float _animationDuration;

        public CrouchCommand(KeyCode associatedKey, float speedValue) : base(associatedKey, speedValue)
        {
        }

        public override string AnimationParameter { get => "isCrouching"; }

        protected override async void ExecuteAction(GameObject gameObject)
        {
            moveScript.StopMovement();
            animator.SetBool(AnimationParameter, true);
            await Task.Delay((int)(_animationDuration / 2 * 1000));
            _rigidBody.simulated = false;
        }

        protected override bool ExecutionCodition(GameObject gameObject)
        {
            return !moveScript.IsMoving && !moveScript.IsJumping && !moveScript.IsCrouching;
        }

        public override void InitializeFields(GameObject gameObject)
        {
            base.InitializeFields(gameObject);
            _rigidBody = gameObject.GetComponent<Rigidbody2D>();
            _animationDuration = animator.GetAnimationClipDuration("player_squat");
        }
    }
}