using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Plugins.Common
{
    public static class Log<T>
    {
        private static Type _type;

        private static Type type
        {
            get
            {
                if (_type == null)
                {
                    _type = typeof(T);
                }

                return _type;
            }
        }

        public static void Info(string message, Object context = null)
        {
            Debug.Log($"[{type}] {message} \n {Environment.StackTrace}", context);
            LogListenersProvider.Track(LogType.Log, $"[{type}] {message}");
        }

        public static void Warn(string message)
        {
            Debug.LogWarning($"[{type}] {message} \n {Environment.StackTrace}");
            LogListenersProvider.Track(LogType.Warning, $"[{type}] {message}");
        }

        public static void Error(string message)
        {
            Debug.LogError($"[{type}] {message} \n {Environment.StackTrace}");
            LogListenersProvider.Track(LogType.Error, $"[{type}] {message}");
        }

        public static void Error(Exception e)
        {
            Debug.LogError($"[{type}] {e.Message} \n {e.StackTrace}");
            LogListenersProvider.Track(LogType.Error, $"[{type}] {e.Message}");
        }
    }

    public static class LogListenersProvider
    {
        private static List<ILogsListener> listeners = new List<ILogsListener>();

        public static void AddListener(ILogsListener listener)
        {
            listeners.Add(listener);
        }

        public static void Track(LogType logType, string log)
        {
            if (listeners.Count > 0)
            {
                foreach (var listener in listeners)
                {
                    listener.Log(logType, log);
                }
            }
        }
    }
}