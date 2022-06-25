using System;
using System.Collections.Generic;

namespace Plugins.Common
{
    public class EventSystem : Singleton<EventSystem>
    {
        private Dictionary<Type, List<Action<IEvent>>> _handlers = new Dictionary<Type, List<Action<IEvent>>>();

        public void Subscribe<T>(Action<IEvent> handler) where T : IEvent
        {
            if (!_handlers.ContainsKey(typeof(T)))
            {
                _handlers.Add(typeof(T), new List<Action<IEvent>>());
            }

            _handlers[typeof(T)].Add(handler);
        }

        public void Unsubscribe<T>(Action<IEvent> handler)
        {
            if (!_handlers.ContainsKey(typeof(T)))
            {
                _handlers[typeof(T)].Remove(handler);
            }
        }

        public void Emmit<T>(T eventObject) where T : IEvent
        {
            if (_handlers.ContainsKey(typeof(T)))
            {
                foreach (var handler in _handlers[typeof(T)])
                {
                    handler.Invoke(eventObject);
                }
            }
        }
    }
}