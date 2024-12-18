using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using PlayablesStudio.Plugins.YandexGamesSDK.Editor.Dashboard;
using PlayablesStudio.Plugins.YandexGamesSDK.Editor.ProxyServer;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.BuildProcessors
{
    public class PostBuildProcessor : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPostprocessBuild(BuildReport report)
        {
            // Check if the build target is WebGL


            if (report.summary.platform != BuildTarget.WebGL)
            {
                return;
            }

            var config = YandexGamesSDKConfig.Instance;

            if (config.developmentSettings.overrideOnBuild)
            {
                config.SetServerConfiguration(report.summary.outputPath, FindAvailablePort());
            }

            if (config.developmentSettings.runLocalServerAfterBuild)
            {
                if (!ProxyServerFactory.ProxyServer.IsRunning)
                {
                    ProxyServerFactory.ProxyServer.StartProxyServer();
                }
            }
        }

        private int FindAvailablePort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }


        private static Process serverProcess;

        private static void StartLocalServer(string buildPath)
        {
            var config = YandexGamesSDKConfig.Instance;
            string port = "8190"; // Use the port you're using

            // Terminate the previous server process if it's still running
            if (serverProcess != null && !serverProcess.HasExited)
            {
                serverProcess.Kill();
                serverProcess.Dispose();
                serverProcess = null;
                UnityEngine.Debug.Log("Previous server process terminated.");
            }

            string npxPath = "/opt/homebrew/bin/npx"; // Full path to npx
            string nodeDirectory = "/opt/homebrew/bin"; // Directory containing node, npm, npx

            string arguments = $"@yandex-games/sdk-dev-proxy -p \"{buildPath}\" --app-id={config.appID} --port={port}";

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = npxPath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = false,
                WorkingDirectory = buildPath,
            };

            // Update PATH
            string currentPath = startInfo.EnvironmentVariables["PATH"];
            startInfo.EnvironmentVariables["PATH"] = $"{nodeDirectory}:{currentPath}";

            UnityEngine.Debug.Log("Process PATH: " + startInfo.EnvironmentVariables["PATH"]);

            try
            {
                serverProcess = new Process();
                serverProcess.StartInfo = startInfo;

                serverProcess.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        UnityEngine.Debug.Log(args.Data);
                    }
                };
                serverProcess.ErrorDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        UnityEngine.Debug.LogError("Process Error: " + args.Data);
                    }
                };

                serverProcess.Start();
                serverProcess.BeginOutputReadLine();
                serverProcess.BeginErrorReadLine();

                UnityEngine.Debug.Log("Local server started successfully.");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("Failed to start local server: " + ex.Message);
            }
        }

        private void OnDestroy()
        {
            // Terminate the server process when the script is destroyed
            if (serverProcess != null && !serverProcess.HasExited)
            {
                serverProcess.Kill();
                serverProcess.Dispose();
                serverProcess = null;
                UnityEngine.Debug.Log("Server process terminated on script destroy.");
            }
        }


        private const string NpmPath = "/opt/homebrew/bin/npm";

        private bool IsNpmInstalled()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = NpmPath,
                    Arguments = "--version",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                };

                Process process = Process.Start(startInfo);
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    UnityEngine.Debug.Log("npm version: " + output);
                    return true;
                }
                else
                {
                    UnityEngine.Debug.LogError("npm returned a non-zero exit code.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("Error checking npm installation: " + ex.Message);
                return false;
            }
        }


        private bool IsPackageInstalledGlobally(string packageName)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = "npm",
                    Arguments = $"list -g {packageName}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                };

                Process process = Process.Start(startInfo);
                process.WaitForExit();
                string output = process.StandardOutput.ReadToEnd();
                return output.Contains(packageName);
            }
            catch
            {
                return false;
            }
        }
    }
}