using System;
using Newtonsoft.Json;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Types
{
    [Serializable]
    public class Player
    {
        [JsonProperty("uniqueID")]
        public string UniqueID { get; set; }

        [JsonProperty("publicName")]
        public string PublicName { get; set; }

        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("scopePermissions")]
        public ScopePermissions ScopePermissions { get; set; }

        [JsonProperty("avatarUrlSmall")]
        public string AvatarUrlSmall { get; set; }

        [JsonProperty("avatarUrlMedium")]
        public string AvatarUrlMedium { get; set; }

        [JsonProperty("avatarUrlLarge")]
        public string AvatarUrlLarge { get; set; }
    }
}