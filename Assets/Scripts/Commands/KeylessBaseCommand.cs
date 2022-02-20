using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Commands
{
    [Serializable]
    public abstract class KeylessBaseCommand
    {
        [SerializeField]
        protected GameObject associatedObject;

        public abstract Task Execute();
    }
}