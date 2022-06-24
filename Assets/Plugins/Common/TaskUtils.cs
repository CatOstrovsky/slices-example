using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Plugins.Common
{
    public static class TaskUtils
    {
        public static async Task<T> ToTask<T>(this T t) where T : Tween {
            var completionSource = new TaskCompletionSource<T>();
            t.OnComplete(() => completionSource.SetResult(t));
            return await completionSource.Task;
        }

        public static void DoNotAwait<T>(this T t) where T : Task
        {
            try
            {
                _= t;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public static Task CallBack<T>(this T t, Action callback) where T : Task
        {
            return Task.Run(async () =>
            {
                try
                {
                    await t;
                    callback?.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    throw;
                }
            });
        }
    }
}