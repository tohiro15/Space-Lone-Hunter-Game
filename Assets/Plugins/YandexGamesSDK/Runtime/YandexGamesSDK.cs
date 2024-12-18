using System;
using System.Runtime.InteropServices;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Logging;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Abstractions;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Advertisement;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Authentication;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Leaderboard;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.LocalStorage;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Storage;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Networking;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Types;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime
{
    [DefaultExecutionOrder(-100)]
    public sealed class YandexGamesSDK : MonoBehaviour
    {
        private static YandexGamesSDK _instance;

        public static YandexGamesSDK Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<YandexGamesSDK>();

                    if (_instance == null)
                    {
                        GameObject sdkObject = new GameObject(nameof(YandexGamesSDK));
                        _instance = sdkObject.AddComponent<YandexGamesSDK>();
                    }
                }

                return _instance;
            }
        }

        [DllImport("__Internal")]
        private static extern void GetServerTime(string requestId);

        [DllImport("__Internal")]
        private static extern void GetEnvironment(string requestId);

        [DllImport("__Internal")]
        private static extern void SetGameplayReady(string requestId);

        [DllImport("__Internal")]
        private static extern void SetGameplayStart(string requestId);

        [DllImport("__Internal")]
        private static extern void SetGameplayStop(string requestId);


        public IAuthenticationModule Authentication { get; private set; }
        public ICloudStorageModule CloudStorage { get; private set; }
        public ILocalStorageModule LocalStorage { get; private set; }
        public ILeaderboardModule Leaderboard { get; private set; }
        public IAdvertisementModule Advertisement { get; private set; }

        private bool _isInitialized = false;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            InitializeModules();
        }

        public void GetServerTime(Action<bool, string, string> callback)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();

            YGRequestManager.RegisterCallback<string>(requestId, (success, data, error) =>
            {
                callback?.Invoke(success, data, error);
            });

            GetServerTime(requestId);
#else
            YGLogger.Debug("GetServerTime is only available in WebGL builds.");
            callback?.Invoke(false, null, "GetServerTime is only available in WebGL builds.");
#endif
        }

        public void GetEnvironment(Action<bool, EnvironmentData, string> callback)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();

            YGRequestManager.RegisterCallback<EnvironmentData>(requestId, (success, data, error) =>
            {
                callback?.Invoke(success, data, error);
            });

            GetEnvironment(requestId);
#else
            YGLogger.Debug("GetEnvironment is only available in WebGL builds.");
            callback?.Invoke(false, null, "GetEnvironment is only available in WebGL builds.");
#endif
        }

        public void SetGameplayReady(Action<bool, string> callback = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();

            YGRequestManager.RegisterCallback<object>(requestId, (success, data, error) =>
            {
                callback?.Invoke(success, error);
            });

            SetGameplayReady(requestId);
#else
            YGLogger.Debug("SetGameplayReady is only available in WebGL builds.");
            callback?.Invoke(false, "SetGameplayReady is only available in WebGL builds.");
#endif
        }

        public void SetGameplayStart(Action<bool, string> callback = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();

            YGRequestManager.RegisterCallback<object>(requestId, (success, data, error) =>
            {
                callback?.Invoke(success, error);
            });

            SetGameplayStart(requestId);
#else
            YGLogger.Debug("SetGameplayStart is only available in WebGL builds.");
            callback?.Invoke(false, "SetGameplayStart is only available in WebGL builds.");
#endif
        }

        public void SetGameplayStop(Action<bool, string> callback = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();

            YGRequestManager.RegisterCallback<object>(requestId, (success, data, error) =>
            {
                callback?.Invoke(success, error);
            });

            SetGameplayStop(requestId);
#else
            YGLogger.Debug("SetGameplayStop is only available in WebGL builds.");
            callback?.Invoke(false, "SetGameplayStop is only available in WebGL builds.");
#endif
        }

        private void InitializeModules()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            LocalStorage = LoadAndInitializeModule<PlayerPrefsLocalStorageModule>();
            Authentication = LoadAndInitializeModule<AuthenticationModule>();
            Advertisement = LoadAndInitializeModule<AdvertisementModule>();
            Leaderboard = LoadAndInitializeModule<LeaderboardModule>();
            CloudStorage = LoadAndInitializeModule<CloudStorageModule>();
#elif UNITY_EDITOR
            Authentication = LoadAndInitializeModule<MockAuthenticationModule>();
            Leaderboard = LoadAndInitializeModule<MockLeaderboardModule>();
            CloudStorage = LoadAndInitializeModule<MockCloudStorageModule>();
            Advertisement = LoadAndInitializeModule<MockAdvertisementModule>();

            YGLogger.Debug("Modules initialized with mock data settings in editor.");
#endif
        }

        private TModule LoadAndInitializeModule<TModule>() where TModule : YGModuleBase
        {
            var module = GetComponent<TModule>() ?? gameObject.AddComponent<TModule>();

            module.Initialize();

            return module;
        }

        // This method will be called from JavaScript
        public void OnJSResponse(string jsonResponse)
        {
            YGRequestManager.HandleJSResponse(jsonResponse);
        }
    }
}