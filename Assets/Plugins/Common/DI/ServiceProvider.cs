using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core;

namespace Plugins.Common
{
    using Log = Log<String>;

    public static class ServiceProvider
    {
        private static Dictionary<Type, object> _stack = new Dictionary<Type, object>();

        public static void Register<T>(T instance)
        {
            _stack.Add(typeof(T), instance);
        }

        public static T Get<T>()
        {
            var type = typeof(T);
            if (_stack.ContainsKey(type))
            {
                return (T)_stack[type];
            }
            else
            {
                Log.Error($"Unable to load provider with type: {type}");
                throw new Exception($"Critical {nameof(ServiceProvider)} error...");
            }
        }

        public static IReadOnlyDictionary<Type, object> GetAll()
        {
            return _stack;
        }
    }
}