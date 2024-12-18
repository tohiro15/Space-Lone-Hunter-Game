using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;
using UnityEditor;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.Dashboard.Windows
{
    public class MockDataWindow : IYandexGamesSDKWindow
    {
        public int Priority => 40;
        public string WindowName => "Mock Data Settings";

        private SerializedObject serializedConfig;
        private SerializedProperty useMockDataProp;
        private SerializedProperty mockDataProp;

        public void OnEnable()
        {
            var config = YandexGamesSDKConfig.Instance;
            serializedConfig = new SerializedObject(config);

            useMockDataProp = serializedConfig.FindProperty("useMockData");
            mockDataProp = serializedConfig.FindProperty("mockData");
        }

        public void OnDisable()
        {
            // Cleanup if necessary
        }

        public void OnGUI()
        {
            serializedConfig.Update();

            EditorGUILayout.LabelField("Mock Data Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(useMockDataProp, new GUIContent("Enable Mock Data"));

            if (useMockDataProp.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(mockDataProp, new GUIContent("Mock Data Configuration"), true);
                EditorGUI.indentLevel--;
            }

            serializedConfig.ApplyModifiedProperties();
        }
    }
}