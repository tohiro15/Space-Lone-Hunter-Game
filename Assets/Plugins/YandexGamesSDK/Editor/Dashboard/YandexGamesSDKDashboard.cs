using System;
using System.Collections.Generic;
using System.Linq;
using PlayablesStudio.Plugins.YandexGamesSDK.Editor.Dashboard.Windows;
using UnityEditor;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.Dashboard
{
    public class YandexGamesSDKDashboard : EditorWindow
    {
        private List<IYandexGamesSDKWindow> windows;
        private string[] windowNames;
        private int selectedWindowIndex = 0;

        [MenuItem("Yandex Games SDK/Dashboard")]
        public static void ShowWindow()
        {
            GetWindow<YandexGamesSDKDashboard>("Yandex Games SDK Dashboard");
        }

        private void OnEnable()
        {
            LoadWindows();

            foreach (var window in windows)
            {
                window.OnEnable();
            }
        }

        private void OnDisable()
        {
            foreach (var window in windows)
            {
                window.OnDisable();
            }
        }

        private void LoadWindows()
        {
            windows = new List<IYandexGamesSDKWindow>();

            var windowTypes = GetAllWindowTypes();
            foreach (var type in windowTypes)
            {
                if (Activator.CreateInstance(type) is IYandexGamesSDKWindow windowInstance)
                {
                    windows.Add(windowInstance);
                }
            }

            windows = windows.OrderBy(w => w.Priority).ToList();

            windowNames = windows.Select(w => w.WindowName).ToArray();
        }

        private IEnumerable<Type> GetAllWindowTypes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var windowTypes = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                    .Where(t => typeof(IYandexGamesSDKWindow).IsAssignableFrom(t)
                                && !t.IsAbstract
                                && t.Namespace != null);

                windowTypes.AddRange(types);
            }

            return windowTypes;
        }


        private void OnGUI()
        {
            EditorGUILayout.Space();
            selectedWindowIndex = GUILayout.Toolbar(selectedWindowIndex, windowNames);

            EditorGUILayout.Space();

            if (selectedWindowIndex >= 0 && selectedWindowIndex < windows.Count)
            {
                windows[selectedWindowIndex].OnGUI();
            }
            else
            {
                EditorGUILayout.LabelField("No windows available.");
            }
        }

        public void FocusWindow(string windowName)
        {
            int index = Array.FindIndex(windowNames, name => name == windowName);
            if (index >= 0)
            {
                selectedWindowIndex = index;
            }
        }
    }
}