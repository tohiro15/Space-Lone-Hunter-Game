using System;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Advertisement
{
    public interface IAdvertisementModule
    {
        void ShowInterstitialAd(Action<bool, YGAdResponse, string> callback = null);
        void ShowRewardedAd(Action<bool, YGAdResponse, string> callback = null);
        void ShowBannerAd(string position, Action<bool, YGAdResponse, string> callback = null);
        void HideBannerAd(Action<bool, YGAdResponse, string> callback = null);
    }
}