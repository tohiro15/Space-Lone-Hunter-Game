using PlayablesStudio.Plugins.YandexGamesSDK.Editor.ProxyServer.Abstractions;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Dashboard;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.ProxyServer
{
    public static class ProxyServerFactory
    {
        public static IProxyServer ProxyServer => _proxyServer ??= new InternalProxyServer(YandexGamesSDKConfig.Instance);

        private static IProxyServer _proxyServer;
    }
}