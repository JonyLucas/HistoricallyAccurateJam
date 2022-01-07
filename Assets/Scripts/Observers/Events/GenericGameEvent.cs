using Game.Observer.Listeners;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects.Events
{
    /// <summary>
    /// This class implements the observer pattern, which calls the methods that are listening to this specific event.
    /// These methods are invoked when this event occurs.
    /// This generic class allows to pass the specific object of type T as an argument to the methods, whenever the event occurs.
    /// </summary>
    public class GenericGameEvent<T> : ScriptableObject
    {
        private readonly List<GenericEventListener<T>> _listeners = new List<GenericEventListener<T>>();

        public void Register(GenericEventListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public void Unregister(GenericEventListener<T> listener)
        {
            _listeners.Remove(listener);
        }

        public void OnOcurred(T instance)
        {
            var listernersCount = _listeners.Count;
            for (int i = 0; i < listernersCount; i++)
            {
                _listeners[i].OnEventOccurs(instance);
            }
        }
    }
}