using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Common
{
    public class DebugMenu : MonoBehaviour
    {
        private const int FontSize = 25;
        private bool _isShow;
        private Vector2 contentScrollPosition = Vector2.zero;
        private Vector2 pageScrollPosition = Vector2.zero;
        private List<DebugMenuPageBase> history = new List<DebugMenuPageBase>();

        private static List<DebugMenuPageBase> debugMenuPages = new List<DebugMenuPageBase>
        {
            new LogsDebugMenuPage()
        };

        [SerializeField] private KeyCode debugMenuKeyCode = KeyCode.E;
        [SerializeField] private Button debugMenuButton;
        [SerializeField] private Image blockerLayer;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            debugMenuButton.onClick.AddListener(ToggleVisible);
        }

        public static void AddPage(DebugMenuPageBase page)
        {
            debugMenuPages.Add(page);
        }

        private void OnGUI()
        {
            GUI.skin.label.fontSize = FontSize;
            GUI.skin.button.fontSize = FontSize;
            GUI.skin.textArea.fontSize = FontSize;
            GUI.skin.textField.fontSize = FontSize;

            if (_isShow)
            {
                GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(Screen.width),
                    GUILayout.Height(Screen.height));

                GUILayout.BeginHorizontal(GUI.skin.box);
                contentScrollPosition = GUILayout.BeginScrollView(contentScrollPosition);
                if (history.Count == 0)
                {
                    foreach (var page in debugMenuPages)
                    {
                        if (GUILayout.Button(page.Name))
                        {
                            GoTo(page);
                        }
                    }
                }
                else
                {
                    var page = history[0];
                    GUILayout.BeginVertical(GUI.skin.box);
                    pageScrollPosition = GUILayout.BeginScrollView(pageScrollPosition);
                    page.OnGUI();
                    GUILayout.EndScrollView();
                    GUILayout.EndVertical();
                }

                GUILayout.EndScrollView();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal(GUI.skin.box);
                if (history.Count > 0)
                {
                    if (GUILayout.Button("Back"))
                    {
                        pageScrollPosition = Vector2.zero;
                        history.RemoveAt(0);
                    }
                }

                if (GUILayout.Button("Exit"))
                {
                    pageScrollPosition = Vector2.zero;
                    history.Clear();
                    ToggleVisible();
                }

                GUILayout.EndHorizontal();


                GUILayout.EndVertical();
            }
        }

        private void GoTo(DebugMenuPageBase pageBase)
        {
            history.Insert(0, pageBase);
        }

        private void ToggleVisible()
        {
            _isShow = !_isShow;
            blockerLayer.enabled = _isShow;
        }

        private void Update()
        {
            if (Input.GetKeyDown(debugMenuKeyCode))
            {
                ToggleVisible();
            }
        }
    }
}