using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Abstractions;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Types;
using UnityEngine;
using Newtonsoft.Json;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Networking;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Leaderboard
{
    public class LeaderboardModule : YGModuleBase, ILeaderboardModule
    {
        [DllImport("__Internal")]
        private static extern void SubmitScore(string requestId, string leaderboardName, int score, string extraData);

        [DllImport("__Internal")]
        private static extern void GetLeaderboardEntries(string requestId, string leaderboardName, int quantityTop, int quantityAround, bool includeUser);

        [DllImport("__Internal")]
        private static extern void GetPlayerEntry(string requestId, string leaderboardName);

        public override void Initialize()
        {
        }

        public virtual void SubmitScore(string leaderboardName, int score, string extraData = null, Action<bool, string> callback = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();
            YGRequestManager.RegisterCallback<string>(requestId, (success, data, error) =>
            {
                if (success)
                {
                    callback?.Invoke(true, null);
                }
                else
                {
                    callback?.Invoke(false, error);
                }
            });

            SubmitScore(requestId, leaderboardName, score, extraData);
#else
            Debug.LogWarning("Submitting scores is only available in WebGL builds.");
            callback?.Invoke(false, "Submitting scores is not supported in this build.");
#endif
        }

        /// <summary>
        /// Retrieves leaderboard entries.
        /// </summary>
        /// <param name="leaderboardName">The name of the leaderboard.</param>
        /// <param name="quantityTop">Number of top entries to fetch.</param>
        /// <param name="quantityAround">Number of entries around the player.</param>
        /// <param name="includeUser">Whether to include the user's entry.</param>
        /// <param name="callback">Callback invoked upon completion with leaderboard data.</param>
        public virtual void GetLeaderboardEntries(string leaderboardName, int quantityTop, int quantityAround, bool includeUser, Action<LeaderboardEntriesResponse, string> callback = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();
            YGRequestManager.RegisterCallback<string>(requestId, (success, data, error) =>
            {
                if (success && !string.IsNullOrEmpty(data))
                {
                    try
                    {
                        var leaderboardEntries = JsonConvert.DeserializeObject<LeaderboardEntriesResponse>(data);
                        callback?.Invoke(leaderboardEntries, null);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Deserialization error: {ex.Message}");
                        callback?.Invoke(null, "Failed to parse leaderboard entries.");
                    }
                }
                else
                {
                    callback?.Invoke(null, error);
                }
            });

            GetLeaderboardEntries(requestId, leaderboardName, quantityTop, quantityAround, includeUser);
#else
            Debug.LogWarning("Fetching leaderboard entries is only available in WebGL builds.");
            callback?.Invoke(null, "Fetching leaderboard entries is not supported in this build.");
#endif
        }

        public virtual void GetPlayerEntry(string leaderboardName, Action<LeaderboardEntry, string> callback = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();
            YGRequestManager.RegisterCallback<string>(requestId, (success, data, error) =>
            {
                if (success && !string.IsNullOrEmpty(data))
                {
                    try
                    {
                        var playerEntry = JsonConvert.DeserializeObject<LeaderboardEntry>(data);
                        callback?.Invoke(playerEntry, null);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Deserialization error: {ex.Message}");
                        callback?.Invoke(null, "Failed to parse player entry.");
                    }
                }
                else
                {
                    callback?.Invoke(null, error);
                }
            });

            GetPlayerEntry(requestId, leaderboardName);
#else
            Debug.LogWarning("Fetching player entry is only available in WebGL builds.");
            callback?.Invoke(null, "Fetching player entry is not supported in this build.");
#endif
        }
    }
}
