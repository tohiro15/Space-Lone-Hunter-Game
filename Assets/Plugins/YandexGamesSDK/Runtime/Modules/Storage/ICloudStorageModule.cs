using System;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Storage
{
    public interface ICloudStorageModule
    {
        void Save<TData>(string key, TData data, Action<bool, string> callback = null);
        void Save(string key, object data, Action<bool, string> callback = null);
        void Load<TData>(string key, Action<bool, TData, string> callback = null);
        void Load(string key, Action<bool, object, string> callback = null);
        void FlushData(Action<bool, string> callback = null);
    }
}