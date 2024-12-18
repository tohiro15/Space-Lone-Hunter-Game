using System;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Editor.ProxyServer.Abstractions
{
    public interface IProxyServer
    {
        public string Logs { get; }
        public event Action<string> OnError;
        public event Action OnLogUpdate;
        
        public bool IsRunning { get; }
        public void StartProxyServer();
        public void StopProxyServer();
        public void Cleanup();
        public void ClearLogs();
    }
}