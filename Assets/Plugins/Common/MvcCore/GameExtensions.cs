using UnityEngine;

namespace Core
{
    public static class GameExtensions
    {
        public static T GetInstance<T>(this T item) where T : ControllerBase
        {
            if (item.gameObject != null)
            {
                return GameObject.Instantiate(item.gameObject).GetComponent<T>();
            }

            return null;
        }
    }
}