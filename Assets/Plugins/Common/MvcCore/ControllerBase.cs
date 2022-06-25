using System;
using Core;
using UnityEngine;

public abstract class ControllerBase :
    MonoBehaviour,
    IDisposable,
    IController
{
    public virtual void Dispose()
    {
        Destroy(gameObject);
    }

    public abstract ViewBase GetView { get; }
}

public interface IController
{
    public ViewBase GetView { get; }
}