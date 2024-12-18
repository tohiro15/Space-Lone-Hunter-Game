using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;
using UnityEditor;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.Dashboard.Windows
{
    public class DevelopmentSettingsWindow : IYandexGamesSDKWindow
    {
        public int Priority => 30;
        public string WindowName => "Development Settings";

        private SerializedObject serializedConfig;
        private SerializedProperty developmentSettingsProp;
        private SerializedProperty serverPortProp;
        private SerializedProperty buildPathProp;
        private SerializedProperty overrideOnBuildProp;
        private SerializedProperty runLocalServerAfterBuildProp;

        public void OnEnable()
        {
            var config = YandexGamesSDKConfig.Instance;
            serializedConfig = new SerializedObject(config);

            developmentSettingsProp = serializedConfig.FindProperty("developmentSettings");
            serverPortProp = developmentSettingsProp.FindPropertyRelative("serverPort");
            buildPathProp = developmentSettingsProp.FindPropertyRelative("buildPath");
            overrideOnBuildProp = developmentSettingsProp.FindPropertyRelative("overrideOnBuild");
            runLocalServerAfterBuildProp = developmentSettingsProp.FindPropertyRelative("runLocalServerAfterBuild");
        }

        public void OnDisable()
        {
            // Cleanup if necessary
        }

        public void OnGUI()
        {
            serializedConfig.Update();

            EditorGUILayout.LabelField("Development Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serverPortProp, new GUIContent("Server Port"));
            EditorGUILayout.PropertyField(buildPathProp, new GUIContent("Build Path"));

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(overrideOnBuildProp, new GUIContent("Override on Build"));
            EditorGUILayout.PropertyField(runLocalServerAfterBuildProp, new GUIContent("Run Local Server After Build"));

            serializedConfig.ApplyModifiedProperties();
        }
    }
}