using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Types
{
    [Serializable]
    public class LeaderboardEntriesResponse
    {
        [JsonProperty("leaderboard")]
        public Leaderboard Leaderboard { get; set; }

        [JsonProperty("ranges")]
        public List<Range> Ranges { get; set; }

        [JsonProperty("userRank")]
        public int UserRank { get; set; }

        [JsonProperty("entries")]
        public List<LeaderboardEntry> Entries { get; set; }
    }

    [Serializable]
    public class Leaderboard
    {
        [JsonProperty("appID")]
        public string AppID { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("description")]
        public LeaderboardDescription Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }
    }

    [Serializable]
    public class LeaderboardDescription
    {
        [JsonProperty("invert_sort_order")]
        public bool InvertSortOrder { get; set; }

        [JsonProperty("score_format")]
        public ScoreFormat ScoreFormat { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    [Serializable]
    public class ScoreFormat
    {
        [JsonProperty("options")]
        public ScoreFormatOptions Options { get; set; }
    }

    [Serializable]
    public class ScoreFormatOptions
    {
        [JsonProperty("decimal_offset")]
        public int DecimalOffset { get; set; }
    }

    [Serializable]
    public class Title
    {
        [JsonProperty("en")]
        public string English { get; set; }

        [JsonProperty("ru")]
        public string Russian { get; set; }

        // Add other languages as needed
    }

    [Serializable]
    public class Range
    {
        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }
    }

    [Serializable]
    public class LeaderboardEntry
    {
        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("extraData")]
        public string ExtraData { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("formattedScore")]
        public string FormattedScore { get; set; }
    }

    [Serializable]
    public class ScopePermissions
    {
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("public_name")]
        public string PublicName { get; set; }
    }
    
    [Serializable]
    public class LeaderboardDataWrapper
    {
        [JsonProperty("entries")]
        public List<LeaderboardEntry> Entries { get; set; }
    }
}
