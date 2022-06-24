using UnityEngine;

namespace Plugins.Common
{
    public abstract class DebugMenuPageBase
    {
        public abstract string Name { get; }
        public abstract void OnGUI();

        public string TextField(string title, string value)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(title);
            value = GUILayout.TextField(value);
            GUILayout.EndHorizontal();
            return value;
        }

        public string TextArea(string title, string value)
        {
            GUILayout.BeginVertical();
            GUILayout.Label(title);
            value = GUILayout.TextArea(value);
            GUILayout.EndVertical();
            return value;
        }
    }
}