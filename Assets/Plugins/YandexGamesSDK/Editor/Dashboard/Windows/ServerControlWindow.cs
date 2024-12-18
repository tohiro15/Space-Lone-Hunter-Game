using PlayablesStudio.Plugins.YandexGamesSDK.Editor.ProxyServer;
using PlayablesStudio.Plugins.YandexGamesSDK.Editor.ProxyServer.Abstractions;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;
using UnityEditor;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.Dashboard.Windows
{
    public class ServerControlWindow : IYandexGamesSDKWindow
    {
        public int Priority => 50;
        public string WindowName => "Server Control";

        private IProxyServer proxyServer;
        private YandexGamesSDKConfig config;
        private Vector2 logScrollPos;
        private string logText = "";
        private bool autoScroll = true;

        public void OnEnable()
        {
            config = YandexGamesSDKConfig.Instance;
            proxyServer = ProxyServerFactory.ProxyServer;
            proxyServer.OnLogUpdate += UpdateLogs;
        }

        public void OnDisable()
        {
            proxyServer.OnLogUpdate -= UpdateLogs;
        }

        public void OnGUI()
        {
            EditorGUILayout.LabelField("Server Control", EditorStyles.boldLabel);

            DrawServerControls();
            DrawServerStatus();
            DrawServerLogs();
        }

        private void DrawServerControls()
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            if (proxyServer.IsRunning)
            {
                if (GUILayout.Button("Stop Server"))
                {
                    proxyServer.StopProxyServer();
                }
            }
            else
            {
                if (GUILayout.Button("Start Server"))
                {
                    StartServer();
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawServerStatus()
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Server Status: ");
            GUIStyle statusStyle = new GUIStyle(EditorStyles.boldLabel);
            if (proxyServer.IsRunning)
            {
                statusStyle.normal.textColor = Color.green;
                GUILayout.Label("Running", statusStyle);
            }
            else
            {
                statusStyle.normal.textColor = Color.red;
                GUILayout.Label("Stopped", statusStyle);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawServerLogs()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Server Logs", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            autoScroll = GUILayout.Toggle(autoScroll, "Auto-Scroll", GUILayout.Width(100));
            if (GUILayout.Button("Clear Logs"))
            {
                proxyServer.ClearLogs();
                logText = "";
            }
            EditorGUILayout.EndHorizontal();

            logScrollPos = EditorGUILayout.BeginScrollView(logScrollPos, GUILayout.Height(300));
            EditorGUILayout.TextArea(logText, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();

            if (autoScroll)
            {
                // EditorWindow.GetWindow<YandexGamesSDKDashboard>().Repaint();
            }
        }

        private void UpdateLogs()
        {
            logText = proxyServer.Logs;
        }

        private void StartServer()
        {
            if (string.IsNullOrEmpty(config.appID))
            {
                if (EditorUtility.DisplayDialog("Invalid App ID", "Please enter the Yandex Games App ID.", "OK"))
                {
                    // Focus on the General Settings window
                    YandexGamesSDKDashboard dashboard = EditorWindow.GetWindow<YandexGamesSDKDashboard>();
                    dashboard.FocusWindow("General Settings");
                }
                return;
            }

            string indexPath = System.IO.Path.Combine(config.developmentSettings.buildPath, "index.html");

            if (!System.IO.File.Exists(indexPath))
            {
                if (EditorUtility.DisplayDialog("Invalid Build Path",
                        "The build path is invalid or does not contain 'index.html'. Would you like to build the project now?",
                        "Yes, Build Now", "Cancel"))
                {
                    config.developmentSettings.overrideOnBuild = true;
                    config.developmentSettings.runLocalServerAfterBuild = true;
                    EditorApplication.ExecuteMenuItem("File/Build And Run");
                }
                return;
            }

            proxyServer.StartProxyServer();
        }
    }
}