using System;
using System.Collections.Generic;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Types;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Leaderboard
{
    public interface ILeaderboardModule
    {
        void SubmitScore(string leaderboardName, int score, string extraData = null, Action<bool, string> callback = null);

        void GetLeaderboardEntries(string leaderboardName, int quantityTop, int quantityAround, bool includeUser, Action<LeaderboardEntriesResponse, string> callback = null);

        void GetPlayerEntry(string leaderboardName, Action<LeaderboardEntry, string> callback = null);
    }
}