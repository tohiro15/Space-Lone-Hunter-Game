using System;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Types;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Authentication
{
    public interface IAuthenticationModule
    {
        void AuthenticateUser(bool requireSignin, Action<bool, string> callback = null);
        UserProfile GetUserProfile();
    }
}