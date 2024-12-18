using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PlayablesStudio.Plugins.YandexGamesSDK.Editor.ProxyServer.Abstractions;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.ProxyServer
{
    public class InternalProxyServer : IProxyServer
    {
        private YandexGamesSDKConfig _config;
        private CancellationTokenSource cancellationTokenSource;
        private Task logRetrievalTask;

        public InternalProxyServer(YandexGamesSDKConfig config)
        {
            _config = config;
        }

        private StringBuilder logBuilder = new StringBuilder();
        public string Logs => logBuilder.ToString(); // Property to retrieve the logs

        public event Action<string> OnError;
        public event Action OnLogUpdate;
        public bool IsRunning { get; private set; }

        [DllImport("libdev_proxy")]
        public static extern void StartServer(string path, bool csp, int port, bool logReq);

        [DllImport("libdev_proxy")]
        public static extern void StopServer();

        [DllImport("libdev_proxy")]
        public static extern IntPtr GetLogs();

        private Task serverTask;

        // Method to start the server with logging and error handling
        public void StartProxyServer()
        {
            if (IsRunning)
            {
                logBuilder.AppendLine("Server is already running.");
                OnLogUpdate?.Invoke();
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                serverTask = Task.Run(() =>
                {
                    try
                    {
                        StartServer(_config.developmentSettings.buildPath, true, _config.developmentSettings.serverPort, true);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                    }
                });

                IsRunning = true;

                // Start log retrieval task
                logRetrievalTask = Task.Run(async () =>
                {
                    while (!cancellationTokenSource.IsCancellationRequested)
                    {
                        RetrieveLogs();
                        await Task.Delay(1000, cancellationTokenSource.Token); // Adjust the delay as needed
                    }
                }, cancellationTokenSource.Token);

                logBuilder.AppendLine("Server started successfully.");

                string encodedGameUrl = Uri.EscapeDataString($"https://localhost:{_config.developmentSettings.serverPort}/");
                string url = $"https://yandex.ru/games/app/{_config.appID}?draft=true&game_url={encodedGameUrl}";
                logBuilder.AppendLine($"You can open your game with {url}");

                OnLogUpdate?.Invoke();

                // Open the URL in the default browser
                OpenUrlInBrowser(url);

                Debug.Log("Server started on a background thread.");
            }
            catch (Exception ex)
            {
                HandleError($"Failed to start the server: {ex.Message}");
            }
        }

        private void OpenUrlInBrowser(string url)
        {
            // This method uses UnityEditor API, so ensure that your code is inside an Editor folder
#if UNITY_EDITOR
            UnityEditor.EditorUtility.OpenWithDefaultApp(url);
#else
            // Fallback for runtime (if needed)
            Application.OpenURL(url);
#endif
        }

        public void RetrieveLogs()
        {
            IntPtr ptr = GetLogs();
            if (ptr != IntPtr.Zero)
            {
                string logs = Marshal.PtrToStringAnsi(ptr);
                Marshal.FreeHGlobal(ptr);

                if (!string.IsNullOrEmpty(logs))
                {
                    logBuilder.Append(logs);
                    OnLogUpdate?.Invoke();
                }
            }
        }

        // Method to stop the server with proper cleanup and logging
        public void StopProxyServer()
        {
            if (!IsRunning)
            {
                logBuilder.AppendLine("Server is not running.");
                OnLogUpdate?.Invoke();
                return;
            }

            try
            {
                if (!cancellationTokenSource.IsCancellationRequested)
                {
                    cancellationTokenSource?.Cancel();
                }

                StopServer();
                IsRunning = false;

                RetrieveLogs(); // Retrieve any remaining logs

                logBuilder.AppendLine("Server stopped successfully.");
                OnLogUpdate?.Invoke();

                Debug.Log("Server stopped.");
            }
            catch (Exception ex)
            {
                HandleError($"Failed to stop the server: {ex.Message}");
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }

        // Method to handle cleanup operations
        public void Cleanup()
        {
            if (IsRunning)
            {
                StopProxyServer();
            }

            logBuilder.Clear();
            OnLogUpdate?.Invoke();
            Debug.Log("Server logs cleared and cleaned up.");
        }

        // Method to clear the log
        public void ClearLogs()
        {
            logBuilder.Clear();
            OnLogUpdate?.Invoke();
            Debug.Log("Logs have been cleared.");
        }

        // Private method to handle error logging and event triggering
        private void HandleError(string errorMessage)
        {
            logBuilder.AppendLine($"ERROR: {errorMessage}");
            OnLogUpdate?.Invoke();
            OnError?.Invoke(errorMessage);

            Debug.LogError(errorMessage);
        }
    }
}
