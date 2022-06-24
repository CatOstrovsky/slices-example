using System;
using System.Collections.Generic;

namespace Plugins.Common
{
    public class EventsBus : Singleton<EventsBus>
    {
        private Dictionary<Type, List<Action<object>>> _handlers = new Dictionary<Type, List<Action<object>>>();

        public void Subscribe<T>(Action<object> handler)
        {
            if (!_handlers.ContainsKey(typeof(T)))
            {
                _handlers.Add(typeof(T), new List<Action<object>>());
            }

            _handlers[typeof(T)].Add(handler);
        }

        public void Unsubscribe<T>(Action<object> handler)
        {
            if (!_handlers.ContainsKey(typeof(T)))
            {
                _handlers[typeof(T)].Remove(handler);
            }
        }

        public void Emmit<T>(object callArgs)
        {
            if (_handlers.ContainsKey(typeof(T)))
            {
                foreach (var handler in _handlers[typeof(T)])
                {
                    handler.Invoke(callArgs);
                }
            }
        }
    }

    public abstract class IEventItem
    {
    }
}