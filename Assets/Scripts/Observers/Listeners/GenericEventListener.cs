using Game.ScriptableObjects.Events;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Observer.Listeners
{
    /// <summary>
    /// The listeners are the part of the observer pattern that listens to a specific event, and when it occurs, the methods that are subscribed will be executed.
    /// This generic class allows to pass the specific object of type T as an argument to the methods, whenever the event occurs.
    /// </summary>
    [Serializable]
    public class GenericEventListener<T> : MonoBehaviour
    {
        [SerializeField]
        private GenericGameEvent<T> _event;

        [SerializeField]
        private UnityEvent<T> _unityEvent;

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
        public void OnEventOccurs(T instance)
        {
            _unityEvent.Invoke(instance);
        }
    }
}