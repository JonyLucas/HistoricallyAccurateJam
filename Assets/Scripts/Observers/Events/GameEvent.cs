using Game.Observer.Listeners;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects.Events
{
    /// <summary>
    /// This class implements the observer pattern, which calls the methods that are listening to this specific event.
    /// These methods are invoked when this event occurs.
    /// </summary>
    [CreateAssetMenu(fileName = "New Event", menuName = "Game Event", order = 54)]
    public class GameEvent : ScriptableObject
    {
        private readonly List<EventListener> _listeners = new List<EventListener>();

        public void Register(EventListener listener)
        {
            _listeners.Add(listener);
        }

        public void Unregister(EventListener listener)
        {
            _listeners.Remove(listener);
        }

        public void OnOcurred()
        {
            var listernersCount = _listeners.Count;
            for (int i = 0; i < listernersCount; i++)
            {
                _listeners[i].OnEventOccurs();
            }
        }
    }
}