using System;
using UnityEngine;

namespace Core
{
    public class ViewBase :
        MonoBehaviour,
        IDisposable
    {
        public virtual void Dispose()
        {
        }
    }
}