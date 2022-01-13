using Game.Player;
using System.Linq;
using UnityEngine;

namespace Game.Commands.Movement
{
    public abstract class BaseMoveCommand : BaseCommand
    {
        [SerializeField]
        protected float speed;

        protected SpriteRenderer renderer;
        protected Animator animator;
        protected PlayerMovement moveScript;

        public abstract string AnimationParameter { get; }
        protected bool isInitialized = false;

        protected BaseMoveCommand(KeyCode associatedKey, float speedValue) : base(associatedKey)
        {
            speed = speedValue;
        }

        public override void Execute(GameObject gameObject)
        {
            if (!isInitialized)
            {
                InitializeFields(gameObject);
            }

            if (ExecutionCodition(gameObject))
            {
                ExecuteAction(gameObject);
            }
        }

        public virtual void InitializeFields(GameObject gameObject)
        {
            renderer = gameObject.GetComponent<SpriteRenderer>();
            animator = gameObject.GetComponent<Animator>();
            moveScript = gameObject.GetComponent<PlayerMovement>();
        }

        protected abstract void ExecuteAction(GameObject gameObject);

        //public abstract void FinishExecution()
    }
}