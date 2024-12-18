using System.IO;
using UnityEditor;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor
{
    [InitializeOnLoad]
    public static class PackageInstaller
    {
        static PackageInstaller()
        {
            CopyWebGLTemplate();
            CopyConfigAsset();
        }

        static void CopyWebGLTemplate()
        {
            string sourceRelativePath = Path.Combine("Runtime", "WebGLTemplates", "YandexGames");
            string destinationPath = Path.Combine(Application.dataPath, "WebGLTemplates", "YandexGames");

            string packagePath = GetPackagePath();

            if (string.IsNullOrEmpty(packagePath))
            {
                Debug.LogError("[YandexGamesSDK] Could not determine the package path.");
                return;
            }

            string sourcePath = Path.Combine(packagePath, sourceRelativePath);

            if (!Directory.Exists(sourcePath))
            {
                Debug.LogError($"[YandexGamesSDK] Source path does not exist: {sourcePath}");
                return;
            }

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);

                CopyDirectory(sourcePath, destinationPath);

                AssetDatabase.Refresh();

                Debug.Log("[YandexGamesSDK] WebGL Template successfully copied to Assets/WebGLTemplates/YandexGames");
            }
            else
            {
                Debug.Log("[YandexGamesSDK] WebGL Template already exists at Assets/WebGLTemplates/YandexGames");
            }
        }

        static void CopyConfigAsset()
        {
            string sourceRelativePath = Path.Combine("Runtime", "Resources", "YandexGamesSDKConfig.asset");
            string destinationDir = Path.Combine(Application.dataPath, "YandexGamesSDK", "Resources");
            string destinationPath = Path.Combine(destinationDir, "YandexGamesSDKConfig.asset");

            string packagePath = GetPackagePath();

            if (string.IsNullOrEmpty(packagePath))
            {
                Debug.LogError("[YandexGamesSDK] Could not determine the package path.");
                return;
            }

            string sourcePath = Path.Combine(packagePath, sourceRelativePath);

            if (!File.Exists(sourcePath))
            {
                Debug.LogError($"[YandexGamesSDK] Config asset does not exist at source path: {sourcePath}");
                return;
            }

            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            if (!File.Exists(destinationPath))
            {
                File.Copy(sourcePath, destinationPath);
                AssetDatabase.Refresh();
                Debug.Log("[YandexGamesSDK] Config asset copied to Assets/YandexGamesSDK/Resources/");
            }
            else
            {
                Debug.Log("[YandexGamesSDK] Config asset already exists at Assets/YandexGamesSDK/Resources/");
            }
        }

        static string GetPackagePath()
        {
            string scriptFilePath = GetScriptFilePath();
            if (string.IsNullOrEmpty(scriptFilePath))
            {
                return null;
            }

            string scriptDir = Path.GetDirectoryName(scriptFilePath);

            string packagePath = Path.GetFullPath(Path.Combine(scriptDir, ".."));

            return packagePath;
        }

        static string GetScriptFilePath()
        {
            string[] guids = AssetDatabase.FindAssets("PackageInstaller t:Script");
            if (guids.Length == 0)
            {
                return null;
            }

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return Path.GetFullPath(path);
        }

        static void CopyDirectory(string sourceDir, string destinationDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDir);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Copy files in the current directory
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(tempPath, true);
            }

            // Copy subdirectories recursively
            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destinationDir, subdir.Name);
                CopyDirectory(subdir.FullName, tempPath);
            }
        }
    }
}
