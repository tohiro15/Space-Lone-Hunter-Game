using System;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Types;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Authentication
{
    public class MockAuthenticationModule : AuthenticationModule
    {
        public override void Initialize()
        {
            LoadMockUserProfile();
        }

        private void LoadMockUserProfile()
        {
            var config = YandexGamesSDKConfig.Instance;
            if (config.useMockData && config.mockData.mockUserProfile != null)
            {
                var mockProfile = config.mockData.mockUserProfile;
                CurrentUser = new UserProfile
                {
                    id = mockProfile.id,
                    name = mockProfile.name,
                    isAuthorized = mockProfile.isAuthorized,
                    avatarUrlSmall = mockProfile.avatarUrlSmall,
                    avatarUrlMedium = mockProfile.avatarUrlMedium,
                    avatarUrlLarge = mockProfile.avatarUrlLarge
                };
                
                Debug.Log("Mock user profile loaded successfully.");
            }
            else
            {
                Debug.LogError("Mock user profile data is not available.");
            }
        }

        public override void AuthenticateUser(bool requireSignin, Action<bool, string> callback = null)
        {
            if (CurrentUser is { isAuthorized: true })
            {
                callback?.Invoke(true, null);
            }
            else
            {
                callback?.Invoke(false, "Mock user is not authorized.");
            }
        }
    }
}