using Game.ScriptableObjects.Events;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Observer.Listeners
{
    /// <summary>
    /// The listeners are the part of the observer pattern that listens to a specific event, and when it occurs, the methods that are subscribed will be executed.
    /// This listener should be used when the subscribed methods don't need an argument to perform its logic.
    /// </summary>
    [Serializable]
    public class EventListener : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _event;

        [SerializeField]
        private UnityEvent _unityEvent;

        // Register to the event when enabled
        private void OnEnable()
        {
            _event.Register(this);
        }

        // Unregister when disabled
        private void OnDisable()
        {
            _event.Unregister(this);
        }

        // Invokes the specific methods that are subscribed to the event.
        public void OnEventOccurs()
        {
            _unityEvent.Invoke();
        }
    }
}