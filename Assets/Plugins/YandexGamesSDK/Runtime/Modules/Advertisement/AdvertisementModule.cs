using System;
using System.Runtime.InteropServices;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Abstractions;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Networking;
using UnityEngine;
using UnityEngine.Events;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Advertisement
{
    public class AdvertisementModule : YGModuleBase, IAdvertisementModule
    {
        private YGPauseSettings _pauseSettings = YandexGamesSDKConfig.Instance.pauseSettings;

        public UnityEvent OnAdOpened = new UnityEvent();
        public UnityEvent OnAdClosed = new UnityEvent();

        private bool originalAudioPause;
        private float originalTimeScale;
        private bool originalCursorVisible;
        private CursorLockMode originalCursorLockMode;

        public override void Initialize()
        {
        }

        [DllImport("__Internal")]
        private static extern void ShowInterstitialAd(string requestId);

        [DllImport("__Internal")]
        private static extern void ShowRewardedAd(string requestId);

        [DllImport("__Internal")]
        private static extern void ShowBannerAd(string requestId, string position);

        [DllImport("__Internal")]
        private static extern void HideBannerAd(string requestId);

        public virtual void ShowInterstitialAd(Action<bool, YGAdResponse, string> callback = null)
        {
    #if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();
            YGRequestManager.RegisterCallback<string>(requestId, (success, data, error) =>
            {
                if (success)
                {
                    if (!string.IsNullOrEmpty(data))
                    {
                        if (Enum.TryParse<YGAdResponse>(data, out var adResponse))
                        {
                            if (adResponse == YGAdResponse.AdOpened)
                                HandleAdOpened();
                            else if (adResponse == YGAdResponse.AdClosed)
                                HandleAdClosed();

                            callback?.Invoke(true, adResponse, null);
                        }
                        else
                        {
                            Debug.LogWarning($"Unknown ad response: {data}");
                            callback?.Invoke(true, default, null);
                        }
                    }
                    else
                    {
                        callback?.Invoke(true, default, null);
                    }
                }
                else
                {
                    callback?.Invoke(false, default, error);
                }
            });

            ShowInterstitialAd(requestId);
    #else
            Debug.Log("Interstitial ads are only available in WebGL builds.");
            callback?.Invoke(false, default, "Interstitial ads are only available in WebGL builds.");
    #endif
        }

        public virtual void ShowRewardedAd(Action<bool, YGAdResponse, string> callback = null)
        {
    #if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();
            YGRequestManager.RegisterCallback<string>(requestId, (success, data, error) =>
            {
                if (success)
                {
                    if (!string.IsNullOrEmpty(data))
                    {
                        if (Enum.TryParse<YGAdResponse>(data, out var adResponse))
                        {
                            if (adResponse == YGAdResponse.AdOpened)
                                HandleAdOpened();
                            else if (adResponse == YGAdResponse.AdClosed)
                                HandleAdClosed();

                            callback?.Invoke(true, adResponse, null);
                        }
                        else
                        {
                            Debug.LogWarning($"Unknown ad response: {data}");
                            callback?.Invoke(true, default, null);
                        }
                    }
                    else
                    {
                        callback?.Invoke(true, default, null);
                    }
                }
                else
                {
                    callback?.Invoke(false, default, error);
                }
            });

            ShowRewardedAd(requestId);
    #else
            Debug.Log("Rewarded ads are only available in WebGL builds.");
            callback?.Invoke(false, default, "Rewarded ads are only available in WebGL builds.");
    #endif
        }

        public virtual void ShowBannerAd(string position, Action<bool, YGAdResponse, string> callback = null)
        {
    #if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();
            YGRequestManager.RegisterCallback<string>(requestId, (success, data, error) =>
            {
                if (success)
                {
                    if (!string.IsNullOrEmpty(data))
                    {
                        if (Enum.TryParse<YGAdResponse>(data, out var adResponse))
                        {
                            callback?.Invoke(true, adResponse, null);
                        }
                        else
                        {
                            Debug.LogWarning($"Unknown ad response: {data}");
                            callback?.Invoke(true, default, null);
                        }
                    }
                    else
                    {
                        callback?.Invoke(true, default, null);
                    }
                }
                else
                {
                    callback?.Invoke(false, default, error);
                }
            });

            ShowBannerAd(requestId, position);
    #else
            Debug.Log("Banner ads are only available in WebGL builds.");
            callback?.Invoke(false, default, "Banner ads are only available in WebGL builds.");
    #endif
        }

        public virtual void HideBannerAd(Action<bool, YGAdResponse, string> callback = null)
        {
    #if UNITY_WEBGL && !UNITY_EDITOR
            string requestId = YGRequestManager.GenerateRequestId();
            YGRequestManager.RegisterCallback<string>(requestId, (success, data, error) =>
            {
                if (success)
                {
                    if (!string.IsNullOrEmpty(data))
                    {
                        if (Enum.TryParse<YGAdResponse>(data, out var adResponse))
                        {
                            callback?.Invoke(true, adResponse, null);
                        }
                        else
                        {
                            Debug.LogWarning($"Unknown ad response: {data}");
                            callback?.Invoke(true, default, null);
                        }
                    }
                    else
                    {
                        callback?.Invoke(true, default, null);
                    }
                }
                else
                {
                    callback?.Invoke(false, default, error);
                }
            });

            HideBannerAd(requestId);
    #else
            Debug.Log("Banner ads are only available in WebGL builds.");
            callback?.Invoke(false, default, "Banner ads are only available in WebGL builds.");
    #endif
        }

        protected virtual void HandleAdOpened()
        {
            originalAudioPause = AudioListener.pause;
            originalTimeScale = Time.timeScale;
            originalCursorVisible = Cursor.visible;
            originalCursorLockMode = Cursor.lockState;

            if (_pauseSettings.pauseAudio)
                AudioListener.pause = true;
            if (_pauseSettings.pauseTime)
                Time.timeScale = 0f;
            if (_pauseSettings.hideCursor)
            {
                Cursor.visible = false;
                Cursor.lockState = _pauseSettings.cursorLockMode;
            }

            OnAdOpened?.Invoke();
        }

        protected virtual void HandleAdClosed()
        {
            AudioListener.pause = originalAudioPause;
            Time.timeScale = originalTimeScale;
            Cursor.visible = originalCursorVisible;
            Cursor.lockState = originalCursorLockMode;

            OnAdClosed?.Invoke();
        }
    }
}
