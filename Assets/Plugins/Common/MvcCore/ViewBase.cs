using System;
using UnityEngine;

namespace Core
{
    public abstract class ViewBase :
        MonoBehaviour,
        IDisposable
    {
        public virtual void Dispose()
        {
        }
    }
}