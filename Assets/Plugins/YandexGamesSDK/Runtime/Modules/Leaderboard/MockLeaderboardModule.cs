using System;
using System.Collections.Generic;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Abstractions;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Types;
using UnityEngine;
using Newtonsoft.Json;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Leaderboard
{
    public class MockLeaderboardModule : LeaderboardModule
    {
        private List<LeaderboardEntry> mockEntries;

        public override void Initialize()
        {
            LoadMockLeaderboardEntries();
        }

        private void LoadMockLeaderboardEntries()
        {
            var config = YandexGamesSDKConfig.Instance;
            if (config.useMockData && config.mockData.mockLeaderboardEntries != null)
            {
                mockEntries = new List<LeaderboardEntry>();

                foreach (var mockEntry in config.mockData.mockLeaderboardEntries)
                {
                    mockEntries.Add(new LeaderboardEntry
                    {
                    });
                }

                Debug.Log("Mock leaderboard entries loaded successfully.");
            }
            else
            {
                Debug.LogWarning("No mock leaderboard data available.");
            }
        }

        public override void SubmitScore(string leaderboardName, int score, string extraData = null,
            Action<bool, string> callback = null)
        {
            Debug.Log($"Mock score submitted for leaderboard '{leaderboardName}': Score = {score}");
            callback?.Invoke(true, null);
        }

        public override void GetLeaderboardEntries(string leaderboardName, int quantityTop, int quantityAround,
            bool includeUser, Action<LeaderboardEntriesResponse, string> callback = null)
        {
            if (mockEntries == null || mockEntries.Count == 0)
            {
                callback?.Invoke(null, "No mock leaderboard data available.");
                return;
            }

            var response = new LeaderboardEntriesResponse
            {
                Entries = mockEntries.GetRange(0, Math.Min(quantityTop, mockEntries.Count))
            };

            Debug.Log("Mock leaderboard entries retrieved.");
            callback?.Invoke(response, null);
        }

        public override void GetPlayerEntry(string leaderboardName, Action<LeaderboardEntry, string> callback = null)
        {
            if (mockEntries is { Count: > 0 })
            {
                var playerEntry = mockEntries[0];
                Debug.Log("Mock player entry retrieved.");
                callback?.Invoke(playerEntry, null);
            }
            else
            {
                callback?.Invoke(null, "No mock player entry data available.");
            }
        }
    }
}