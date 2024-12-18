mergeInto(LibraryManager.library, {

  AuthenticateUser: function(requestIdPtr, requireSigninPtr) {
    if (typeof window.YandexSDKExports.AuthenticateUser === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
var requireSignin = requireSigninPtr;
      window.YandexSDKExports.AuthenticateUser(requestId, requireSignin);
    } else {
      console.error('AuthenticateUser is not defined on window.YandexSDKExports.');
    }
  },

  SavePlayerData: function(requestIdPtr, keyPtr, dataPtr) {
    if (typeof window.YandexSDKExports.SavePlayerData === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
var key = UTF8ToString(keyPtr);
var data = UTF8ToString(dataPtr);
      window.YandexSDKExports.SavePlayerData(requestId, key, data);
    } else {
      console.error('SavePlayerData is not defined on window.YandexSDKExports.');
    }
  },

  LoadPlayerData: function(requestIdPtr, keyPtr) {
    if (typeof window.YandexSDKExports.LoadPlayerData === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
var key = UTF8ToString(keyPtr);
      window.YandexSDKExports.LoadPlayerData(requestId, key);
    } else {
      console.error('LoadPlayerData is not defined on window.YandexSDKExports.');
    }
  },

  CheckForInitialization: function() {
    if (typeof window.YandexSDKExports.CheckForInitialization === 'function') {
      
      window.YandexSDKExports.CheckForInitialization();
    } else {
      console.error('CheckForInitialization is not defined on window.YandexSDKExports.');
    }
  },

  OnYandexGamesSDKReady: function() {
    if (typeof window.YandexSDKExports.OnYandexGamesSDKReady === 'function') {
      
      window.YandexSDKExports.OnYandexGamesSDKReady();
    } else {
      console.error('OnYandexGamesSDKReady is not defined on window.YandexSDKExports.');
    }
  },

  GetServerTime: function(requestIdPtr) {
    if (typeof window.YandexSDKExports.GetServerTime === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
      window.YandexSDKExports.GetServerTime(requestId);
    } else {
      console.error('GetServerTime is not defined on window.YandexSDKExports.');
    }
  },

  GetEnvironment: function(requestIdPtr) {
    if (typeof window.YandexSDKExports.GetEnvironment === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
      window.YandexSDKExports.GetEnvironment(requestId);
    } else {
      console.error('GetEnvironment is not defined on window.YandexSDKExports.');
    }
  },

  SubmitScore: function(requestIdPtr, leaderboardNamePtr, scorePtr, extraDataPtr) {
    if (typeof window.YandexSDKExports.SubmitScore === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
var leaderboardName = UTF8ToString(leaderboardNamePtr);
var score = scorePtr;
var extraData = extraDataPtr;
      window.YandexSDKExports.SubmitScore(requestId, leaderboardName, score, extraData);
    } else {
      console.error('SubmitScore is not defined on window.YandexSDKExports.');
    }
  },

  GetLeaderboardEntries: function(requestIdPtr, leaderboardNamePtr, quantityTopPtr, quantityAroundPtr, includeUserPtr) {
    if (typeof window.YandexSDKExports.GetLeaderboardEntries === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
var leaderboardName = UTF8ToString(leaderboardNamePtr);
var quantityTop = quantityTopPtr;
var quantityAround = quantityAroundPtr;
var includeUser = includeUserPtr;
      window.YandexSDKExports.GetLeaderboardEntries(requestId, leaderboardName, quantityTop, quantityAround, includeUser);
    } else {
      console.error('GetLeaderboardEntries is not defined on window.YandexSDKExports.');
    }
  },

  GetPlayerEntry: function(requestIdPtr, leaderboardNamePtr) {
    if (typeof window.YandexSDKExports.GetPlayerEntry === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
var leaderboardName = UTF8ToString(leaderboardNamePtr);
      window.YandexSDKExports.GetPlayerEntry(requestId, leaderboardName);
    } else {
      console.error('GetPlayerEntry is not defined on window.YandexSDKExports.');
    }
  },

  SetGameplayReady: function(requestIdPtr) {
    if (typeof window.YandexSDKExports.SetGameplayReady === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
      window.YandexSDKExports.SetGameplayReady(requestId);
    } else {
      console.error('SetGameplayReady is not defined on window.YandexSDKExports.');
    }
  },

  SetGameplayStart: function(requestIdPtr) {
    if (typeof window.YandexSDKExports.SetGameplayStart === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
      window.YandexSDKExports.SetGameplayStart(requestId);
    } else {
      console.error('SetGameplayStart is not defined on window.YandexSDKExports.');
    }
  },

  SetGameplayStop: function(requestIdPtr) {
    if (typeof window.YandexSDKExports.SetGameplayStop === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
      window.YandexSDKExports.SetGameplayStop(requestId);
    } else {
      console.error('SetGameplayStop is not defined on window.YandexSDKExports.');
    }
  },

  HideBannerAd: function(requestIdPtr) {
    if (typeof window.YandexSDKExports.HideBannerAd === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
      window.YandexSDKExports.HideBannerAd(requestId);
    } else {
      console.error('HideBannerAd is not defined on window.YandexSDKExports.');
    }
  },

  ShowBannerAd: function(requestIdPtr, positionPtr) {
    if (typeof window.YandexSDKExports.ShowBannerAd === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
var position = UTF8ToString(positionPtr);
      window.YandexSDKExports.ShowBannerAd(requestId, position);
    } else {
      console.error('ShowBannerAd is not defined on window.YandexSDKExports.');
    }
  },

  ShowInterstitialAd: function(requestIdPtr) {
    if (typeof window.YandexSDKExports.ShowInterstitialAd === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
      window.YandexSDKExports.ShowInterstitialAd(requestId);
    } else {
      console.error('ShowInterstitialAd is not defined on window.YandexSDKExports.');
    }
  },

  ShowRewardedAd: function(requestIdPtr) {
    if (typeof window.YandexSDKExports.ShowRewardedAd === 'function') {
      var requestId = UTF8ToString(requestIdPtr);
      window.YandexSDKExports.ShowRewardedAd(requestId);
    } else {
      console.error('ShowRewardedAd is not defined on window.YandexSDKExports.');
    }
  },
});