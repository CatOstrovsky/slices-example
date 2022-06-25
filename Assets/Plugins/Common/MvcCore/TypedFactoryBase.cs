using System;
using System.Collections.Generic;
using Plugins.Common;
using UnityEngine;

namespace Core
{
    using Log = Log<String>;

    public class TypedFactoryBase<T, T2> : MonoBehaviour
        where T : ControllerBase
        where T2 : Enum
    {
        [Serializable]
        public class TypedFactoryOption
        {
            public T unitPrefab;
            public T2 type;
        }

        public List<TypedFactoryOption> units = new List<TypedFactoryBase<T, T2>.TypedFactoryOption>();

        public virtual T GetUnit(T2 type)
        {
            var unitOption = units.Find(unit => unit.type.Equals(type));
            if (unitOption == null)
            {
                Log.Error($"Unable to find a unit with type {type}");
                return null;
            }

            var unit = unitOption.unitPrefab.GetInstance();
            unit.transform.SetParent(transform);
            unit.transform.localScale = Vector3.one;
            unit.transform.position = transform.position;
            return unit;
        }
    }
}