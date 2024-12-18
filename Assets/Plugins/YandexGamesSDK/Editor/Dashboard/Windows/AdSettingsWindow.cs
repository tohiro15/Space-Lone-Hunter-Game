using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;
using UnityEditor;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.Dashboard.Windows
{
    public class AdSettingsWindow : IYandexGamesSDKWindow
    {
        public int Priority => 20;
        public string WindowName => "Ad Settings";

        private SerializedObject serializedConfig;
        private SerializedProperty pauseSettingsProp;
        
        private SerializedProperty pauseSoundOnAdProp;
        private SerializedProperty pauseTimeOnAdProp;
        private SerializedProperty hideCursorOnAdProp;
        private SerializedProperty cursorLockModeOnAdProp;

        public void OnEnable()
        {
            var config = YandexGamesSDKConfig.Instance;
            serializedConfig = new SerializedObject(config);

            pauseSettingsProp = serializedConfig.FindProperty("pauseSettings");
            pauseSoundOnAdProp = pauseSettingsProp.FindPropertyRelative("pauseAudio");
            pauseTimeOnAdProp = pauseSettingsProp.FindPropertyRelative("pauseTime");
            hideCursorOnAdProp = pauseSettingsProp.FindPropertyRelative("hideCursor");
            cursorLockModeOnAdProp = pauseSettingsProp.FindPropertyRelative("cursorLockMode");
        }

        public void OnDisable()
        {
            // Cleanup if necessary
        }

        public void OnGUI()
        {
            serializedConfig.Update();

            EditorGUILayout.LabelField("Ad Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(pauseTimeOnAdProp, new GUIContent("Pause Game During Ads"));
            EditorGUILayout.PropertyField(pauseSoundOnAdProp, new GUIContent("Pause Sound During Ads"));
            EditorGUILayout.PropertyField(hideCursorOnAdProp, new GUIContent("Hide Cursor During Ads"));
            EditorGUILayout.PropertyField(cursorLockModeOnAdProp, new GUIContent("Cursor Lock Mode"));

            serializedConfig.ApplyModifiedProperties();
        }
    }
}