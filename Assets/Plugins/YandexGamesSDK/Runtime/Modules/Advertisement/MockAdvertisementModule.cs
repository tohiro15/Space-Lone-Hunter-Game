using System;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Abstractions;
using UnityEngine;
using UnityEngine.Events;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Advertisement
{
    public class MockAdvertisementModule : AdvertisementModule
    {
        public override void Initialize()
        {
            Debug.Log("Mock Advertisement Module initialized.");
        }

        public override void ShowInterstitialAd(Action<bool, YGAdResponse, string> callback = null)
        {
            Debug.Log("Mock ShowInterstitialAd called.");
            SimulateAdFlow(YGAdResponse.AdOpened, YGAdResponse.AdClosed, callback);
        }

        public override void ShowRewardedAd(Action<bool, YGAdResponse, string> callback = null)
        {
            Debug.Log("Mock ShowRewardedAd called.");
            SimulateAdFlow(YGAdResponse.AdOpened, YGAdResponse.AdClosed, callback);
        }

        public override void ShowBannerAd(string position, Action<bool, YGAdResponse, string> callback = null)
        {
            Debug.Log($"Mock ShowBannerAd called at position: {position}");
            SimulateAdResponse(YGAdResponse.AdOpened, callback);
        }

        public override void HideBannerAd(Action<bool, YGAdResponse, string> callback = null)
        {
            Debug.Log("Mock HideBannerAd called.");
            SimulateAdResponse(YGAdResponse.AdClosed, callback);
        }

        private void SimulateAdFlow(YGAdResponse adOpenResponse, YGAdResponse adCloseResponse, Action<bool, YGAdResponse, string> callback)
        {
            // Simulate ad opened
            HandleAdOpened();
            callback?.Invoke(true, adOpenResponse, null);

            // Simulate ad closed after a delay (e.g., 2 seconds)
            Invoke(nameof(SimulateAdClosed), 2f);
            
            return;

            void SimulateAdClosed()
            {
                HandleAdClosed();
                callback?.Invoke(true, adCloseResponse, null);
            }
        }

        private void SimulateAdResponse(YGAdResponse adResponse, Action<bool, YGAdResponse, string> callback)
        {
            callback?.Invoke(true, adResponse, null);
        }

        protected override void HandleAdOpened()
        {
            Debug.Log("Mock Ad Opened.");
            base.HandleAdOpened();
        }

        protected override void HandleAdClosed()
        {
            Debug.Log("Mock Ad Closed.");
            base.HandleAdClosed();
        }
    }
}
