using System;
using System.Collections.Generic;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Addons;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Advertisement;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Singletons;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard
{
    [Serializable]
    public record DevelopmentSettings
    {
        public bool runLocalServerAfterBuild = false;
        public bool overrideOnBuild = true;
        public string buildPath;
        public int serverPort = 8080;
    }

    [Serializable]
    public record MockDataSettings
    {
        [Header("Mock User Profile")] public MockUserProfile mockUserProfile;

        [Header("Mock Leaderboard Data")] public List<MockLeaderboardEntry> mockLeaderboardEntries;
    }

    [Serializable]
    public record MockUserProfile
    {
        public string id;
        public string name;
        public bool isAuthorized;
        public string avatarUrlSmall;
        public string avatarUrlMedium;
        public string avatarUrlLarge;
    }

    [Serializable]
    public record MockLeaderboardEntry
    {
        public string playerId;
        public string playerName;
        public int score;
    }

    [CreateAssetMenu(fileName = "YandexGamesSDKConfig", menuName = "Yandex Games SDK/Config", order = 1)]
    public sealed class YandexGamesSDKConfig : ScriptableObjectSingleton<YandexGamesSDKConfig>
    {
        [Header("General Settings")] public string appID = "YOUR_GAME_ID";
        public bool isYandexPlatform = false;
        public bool verboseLogging = false;

        public bool useMockData = true;
        [Header("Mock Settings")] public MockDataSettings mockData;

        [Header("Advertisement Settings")] public YGPauseSettings pauseSettings;

        [Header("Development Settings")] public DevelopmentSettings developmentSettings;

        private static Dictionary<string, ScriptableObject> _addonSettings = new Dictionary<string, ScriptableObject>();

        public void SetServerConfiguration(string buildPath, int serverPort)
        {
            if (developmentSettings.overrideOnBuild)
            {
                developmentSettings.buildPath = buildPath;
                developmentSettings.serverPort = serverPort;
            }
        }

        public static void RegisterSettings<T>(string key, T settings) where T : ScriptableObject, IAddonSettings
        {
            if (!_addonSettings.ContainsKey(key))
            {
                _addonSettings.Add(key, settings);
            }
        }

        public static T GetSettings<T>(string key) where T : ScriptableObject, IAddonSettings
        {
            if (_addonSettings.TryGetValue(key, out ScriptableObject settings))
            {
                return settings as T;
            }

            return null;
        }
    }
}