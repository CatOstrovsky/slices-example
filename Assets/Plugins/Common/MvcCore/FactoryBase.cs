using UnityEngine;

namespace Core
{
    public abstract class FactoryBase<T> : MonoBehaviour where T : ControllerBase
    {
        public T unitPrefab;

        public virtual T GetUnit()
        {
            var unit = unitPrefab.GetInstance();
            unit.transform.SetParent(transform);
            unit.transform.localScale = Vector3.one;
            unit.transform.position = transform.position;
            return unit;
        }
    }
}