using System;
using UnityEngine;

namespace Game.Commands
{
    [Serializable]
    public abstract class BaseCommand
    {
        [SerializeField]
        private KeyCode _associatedKey;

        public KeyCode AssociatedKey
        {
            get { return _associatedKey; }
        }

        protected BaseCommand(KeyCode associatedKey)
        {
            _associatedKey = associatedKey;
        }

        /// <summary>
        /// Execute an action implemented by the command pattern.
        /// The idea is that the action is executed when the associated key is pressed.
        /// </summary>
        /// <param name="gameObject"></param>
        public abstract void Execute(GameObject gameObject);

        protected abstract bool ExecutionCodition(GameObject gameObject);
    }
}