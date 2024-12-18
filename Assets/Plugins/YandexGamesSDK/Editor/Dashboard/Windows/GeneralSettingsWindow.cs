using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;
using UnityEditor;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.Dashboard.Windows
{
    public class GeneralSettingsWindow : IYandexGamesSDKWindow
    {
        public int Priority => 10;
        public string WindowName => "General Settings";

        private SerializedObject serializedConfig;
        private SerializedProperty appIDProp;
        private SerializedProperty verboseLoggingProp;

        public void OnEnable()
        {
            var config = YandexGamesSDKConfig.Instance;
            serializedConfig = new SerializedObject(config);
            appIDProp = serializedConfig.FindProperty("appID");
            verboseLoggingProp = serializedConfig.FindProperty("verboseLogging");
        }

        public void OnDisable()
        {
            // Cleanup if necessary
        }

        public void OnGUI()
        {
            serializedConfig.Update();

            EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(appIDProp, new GUIContent("App ID"));
            EditorGUILayout.PropertyField(verboseLoggingProp, new GUIContent("Verbose Logging"));

            serializedConfig.ApplyModifiedProperties();
        }
    }
}