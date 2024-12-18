using System;
using System.Runtime.InteropServices;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Abstractions;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Networking;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Types;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Authentication
{
    public class AuthenticationModule : YGModuleBase, IAuthenticationModule
    {
        public UserProfile CurrentUser { get; protected set; }

        [DllImport("__Internal")]
        private static extern void AuthenticateUser(string requestId, bool requireSignin);

        public  override void Initialize()
        {
            
        }

        public virtual void AuthenticateUser(bool requireSignin, Action<bool, string> callback = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();
            YGRequestManager.RegisterCallback<string>(requestId, (success, data, error) =>
            {
                if (success && !string.IsNullOrEmpty(data))
                {
                    // Deserialize the profile data
                    CurrentUser = JsonUtility.FromJson<UserProfile>(data);
                    callback?.Invoke(true, null);
                }
                else
                {
                    Debug.LogError($"Authentication failed: {error}");
                    callback?.Invoke(false, error);
                }
            });

            AuthenticateUser(requestId, requireSignin);
#else
            Debug.Log("Authentication is only available in WebGL builds.");
            callback?.Invoke(false, "Authentication is only available in WebGL builds.");
            
            
#endif
        }

        public UserProfile GetUserProfile()
        {
            return CurrentUser;
        }
    }
}