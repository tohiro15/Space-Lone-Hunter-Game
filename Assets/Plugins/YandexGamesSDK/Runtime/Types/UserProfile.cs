using System;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Utilities;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Types
{
    [Serializable]
    public class UserProfile
    {
        public string id;
        public string name;
        public string avatarUrlSmall;
        public string avatarUrlMedium;
        public string avatarUrlLarge;
        public bool isAuthorized;
    }
}