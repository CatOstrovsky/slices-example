using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Common
{
    public class LogsDebugMenuPage : DebugMenuPageBase, ILogsListener
    {
        private const int Limit = 50;
        public override string Name => "Logs";
        private static readonly List<LogListItem> _logs = new List<LogListItem>();
        private Vector2 scrollPosition = Vector2.up;

        public LogsDebugMenuPage()
        {
            LogListenersProvider.AddListener(this);
        }

        public void Log(LogType type, string log)
        {
            if (_logs.Count > Limit)
            {
                _logs.RemoveAt(_logs.Count - 1);
            }

            _logs.Insert(0, new LogListItem(type, log));
        }

        public override void OnGUI()
        {
            var backupColor = GUI.contentColor;

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.BeginVertical();
            foreach (var log in _logs)
            {
                GUILayout.BeginVertical(GUI.skin.box);
                switch (log.type)
                {
                    case LogType.Error:
                        GUI.contentColor = Color.red;
                        break;
                    case LogType.Log:
                        GUI.contentColor = Color.white;
                        break;
                    case LogType.Warning:
                        GUI.contentColor = Color.yellow;
                        break;
                }

                GUILayout.TextArea($"[{log.type}] - {log.log}");
                GUILayout.EndVertical();
            }

            GUILayout.EndVertical();
            GUILayout.EndScrollView();

            GUI.contentColor = backupColor;
        }

        private class LogListItem
        {
            public readonly LogType type;
            public readonly string log;

            public LogListItem(LogType type, string log)
            {
                this.type = type;
                this.log = log;
            }
        }
    }
}