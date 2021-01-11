using System;
using System.Reflection;
using UnityEngine;

namespace SebaFixes.config
{
    public sealed class DebugMenu
    {
        private static readonly DebugMenu _debugMenu = new DebugMenu();
        private Vector2 scrollposition = Vector2.zero;
        private Rect UI;

        static DebugMenu()
        {
        }

        private DebugMenu()
        {
        }

        public static DebugMenu Instance
        {
            get
            {
                return _debugMenu;
            }
        }

        public void Show()
        {
            CalculateWindowRect();
            UI = GUILayout.Window(589, UI, DebugMenuUI, "Debug Menu");
        }

        private void DebugMenuUI(int windowId)
        {
            GUILayout.BeginVertical(GUI.skin.box);
            {
                scrollposition = GUILayout.BeginScrollView(scrollposition, false, true);
                {
                    GUILayout.BeginVertical();
                    {
                        GUILayout.FlexibleSpace();
                        GUILayout.Label("DebugVars: ");
                        DebugVars.DoGUI();
                        GUILayout.Space(15);
                        GUILayout.Label("CareerDebug: ");
                        new CareerDebug().DoCareer();
                    }
                    GUILayout.EndVertical();
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndVertical();
        }
        
        private void CalculateWindowRect()
        {
            var width = Mathf.Min(Screen.width, 800);
            var height = Screen.height < 560 ? Screen.height : Screen.height - 100;
            var offsetX = Mathf.RoundToInt((Screen.width - width) / 2f);
            var offsetY = Mathf.RoundToInt((Screen.height - height) / 2f);
            UI = new Rect(offsetX, offsetY, width, height);
        }
    }
}