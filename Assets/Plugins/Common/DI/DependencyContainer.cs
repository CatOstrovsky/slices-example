using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plugins.Common.DI
{
    public class DependencyContainer
    {
        private Dictionary<Type, Type> _Map = new Dictionary<Type, Type>();

        public void Register<TypeToResolve, ResolvedType>()
        {
            _Map.Add(typeof(TypeToResolve), typeof(ResolvedType));
        }

        public T Resolve<T>(params object[] constructorParameters)
        {
            return (T)Resolve(typeof(T), constructorParameters);
        }

        public object Resolve(Type typeToResolve, params object[] constructorParameters)
        {
            Type resolvedType = _Map[typeToResolve];
            ConstructorInfo ctorInfo = resolvedType.GetConstructors().First();
            object retObject = ctorInfo.Invoke(constructorParameters);
            return retObject;
        }
    }
}