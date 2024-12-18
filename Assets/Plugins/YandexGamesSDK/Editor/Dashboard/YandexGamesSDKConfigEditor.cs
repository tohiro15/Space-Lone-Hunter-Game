using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;
using UnityEditor;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.Dashboard
{
    // [CustomEditor(typeof(YandexGamesSDKConfig))]
    // public class YandexGamesSDKConfigEditor : UnityEditor.Editor
    // {
    //     private SerializedProperty appIDProp;
    //     private SerializedProperty verboseLoggingProp;
    //     private SerializedProperty useMockDataProp;
    //     private SerializedProperty mockDataProp;
    //     private SerializedProperty developmentSettingsProp;
    //     private SerializedProperty pauseSettings;
    //
    //     private void OnEnable()
    //     {
    //         appIDProp = serializedObject.FindProperty("appID");
    //         verboseLoggingProp = serializedObject.FindProperty("verboseLogging");
    //         useMockDataProp = serializedObject.FindProperty("useMockData");
    //         mockDataProp = serializedObject.FindProperty("mockData");
    //         developmentSettingsProp = serializedObject.FindProperty("developmentSettings");
    //         pauseSettings = serializedObject.FindProperty("pauseSettings");
    //     }
    //
    //     public override void OnInspectorGUI()
    //     {
    //         serializedObject.Update();
    //
    //         EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);
    //         EditorGUILayout.PropertyField(appIDProp);
    //         EditorGUILayout.PropertyField(verboseLoggingProp);
    //
    //         EditorGUILayout.Space();
    //
    //         EditorGUILayout.LabelField("Mock Data", EditorStyles.boldLabel);
    //         EditorGUILayout.PropertyField(useMockDataProp);
    //         if (useMockDataProp.boolValue)
    //         {
    //             EditorGUILayout.PropertyField(mockDataProp, true);
    //         }
    //
    //         EditorGUILayout.Space();
    //         
    //         EditorGUILayout.LabelField("Pause Settings", EditorStyles.boldLabel);
    //         EditorGUILayout.PropertyField(pauseSettings, true);
    //         
    //         EditorGUILayout.Space();
    //
    //         EditorGUILayout.LabelField("Development Settings", EditorStyles.boldLabel);
    //         EditorGUILayout.PropertyField(developmentSettingsProp, true);
    //
    //         serializedObject.ApplyModifiedProperties();
    //     }
    // }
}