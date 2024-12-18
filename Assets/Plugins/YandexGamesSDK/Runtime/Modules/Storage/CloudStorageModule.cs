using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Logging;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Abstractions;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.LocalStorage;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Networking;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Storage
{
    public class CloudStorageModule : YGModuleBase, ICloudStorageModule
    {
        protected Dictionary<string, string> dataCache = new Dictionary<string, string>();
        protected readonly object cacheLock = new object();
        protected Dictionary<string, long> dataVersions = new Dictionary<string, long>();
        protected bool isDirty = false;

        protected ILocalStorageModule localStorage;

        private const string DataCacheKey = "CloudStorage_DataCache";
        private const string DataVersionsKey = "CloudStorage_DataVersions";

        public override void Initialize()
        {
            localStorage = YandexGamesSDK.Instance.LocalStorage;

            LoadFromLocalStorage();

            LoadAllDataFromBackend((success, error) =>
            {
                if (success)
                {
                    YGLogger.Info("Data loaded successfully during initialization.");
                    isDirty = false;
                }
                else
                {
                    YGLogger.Error($"Failed to load data during initialization: {error}");
                    // Optionally handle errors
                }
            });
        }

        public virtual void Save<T>(string key, T data, Action<bool, string> callback = null)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(data);
                string encryptedData = EncryptData(jsonData);

                lock (cacheLock)
                {
                    dataCache[key] = encryptedData;
                    dataVersions[key] = DateTime.UtcNow.Ticks;
                    isDirty = true;
                }

                SaveToLocalStorage();

                callback?.Invoke(true, null);
            }
            catch (Exception ex)
            {
                YGLogger.Error($"Serialization error for key '{key}': {ex.Message}");
                callback?.Invoke(false, $"Serialization error: {ex.Message}");
            }
        }

        public virtual void Save(string key, object data, Action<bool, string> callback = null)
        {
            Save<object>(key, data, callback);
        }

        public virtual void Load<T>(string key, Action<bool, T, string> callback = null)
        {
            lock (cacheLock)
            {
                if (dataCache.ContainsKey(key))
                {
                    try
                    {
                        string encryptedData = dataCache[key];
                        string jsonData = DecryptData(encryptedData);
                        var data = JsonConvert.DeserializeObject<T>(jsonData);
                        callback?.Invoke(true, data, null);
                    }
                    catch (Exception ex)
                    {
                        YGLogger.Error($"Deserialization error for key '{key}': {ex.Message}");
                        callback?.Invoke(false, default(T), $"Deserialization error: {ex.Message}");
                    }

                    return;
                }
            }

            LoadDataFromBackend<T>(key, callback);
        }

        public virtual void Load(string key, Action<bool, object, string> callback = null)
        {
            Load<object>(key, callback);
        }

        public virtual void FlushData(Action<bool, string> callback = null)
        {
            if (!isDirty)
            {
                YGLogger.Info("No data to flush.");
                callback?.Invoke(true, null);
                return;
            }

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                YGLogger.Warning("No internet connection. Flush postponed.");
                callback?.Invoke(false, "No internet connection.");
                return;
            }

            Dictionary<string, VersionedData> dataToFlush;

            lock (cacheLock)
            {
                dataToFlush = new Dictionary<string, VersionedData>();
                foreach (var key in dataCache.Keys)
                {
                    dataToFlush[key] = new VersionedData
                    {
                        Data = dataCache[key],
                        Version = dataVersions.ContainsKey(key) ? dataVersions[key] : 0
                    };
                }
            }

            SaveToBackend(dataToFlush, (success, error) =>
            {
                if (success)
                {
                    isDirty = false;
                    YGLogger.Info("Data successfully flushed to backend.");
                    callback?.Invoke(true, null);
                }
                else
                {
                    YGLogger.Error($"Failed to flush data to backend: {error}");
                    callback?.Invoke(false, error);
                }
            });
        }

        private void SaveToBackend(Dictionary<string, VersionedData> data, Action<bool, string> callback)
        {
            string requestId = YGRequestManager.GenerateRequestId();

            YGRequestManager.RegisterCallback<string>(requestId,
                (success, _, error) => { callback?.Invoke(success, error); });

            string jsonData = JsonConvert.SerializeObject(data);

#if UNITY_WEBGL && !UNITY_EDITOR
            SavePlayerData(requestId, "yg_data", jsonData);
#else
            YGLogger.Info("Cloud storage is only available in WebGL builds.");
            callback?.Invoke(false, "Cloud storage is only available in WebGL builds.");
#endif
        }

        private void LoadAllDataFromBackend(Action<bool, string> callback = null)
        {
            string requestId = YGRequestManager.GenerateRequestId();

            YGRequestManager.RegisterCallback<string>(requestId, (success, dataJson, error) =>
            {
                if (success)
                {
                    try
                    {
                        var backendData = JsonConvert.DeserializeObject<Dictionary<string, VersionedData>>(dataJson);
                        ResolveConflicts(backendData);
                        SaveToLocalStorage();
                        callback?.Invoke(true, null);
                    }
                    catch (Exception ex)
                    {
                        YGLogger.Error($"Deserialization error: {ex.Message}");
                        callback?.Invoke(false, $"Deserialization error: {ex.Message}");
                    }
                }
                else
                {
                    callback?.Invoke(false, error);
                }
            });

#if UNITY_WEBGL && !UNITY_EDITOR
            LoadPlayerData(requestId, "yg_data");
#else
            YGLogger.Info("Cloud storage is only available in WebGL builds.");
            callback?.Invoke(false, "Cloud storage is only available in WebGL builds.");
#endif
        }

        private void LoadDataFromBackend<T>(string key, Action<bool, T, string> callback)
        {
            LoadAllDataFromBackend((success, error) =>
            {
                if (success)
                {
                    Load<T>(key, callback);
                }
                else
                {
                    callback?.Invoke(false, default(T), error);
                }
            });
        }

        private void ResolveConflicts(Dictionary<string, VersionedData> backendData)
        {
            lock (cacheLock)
            {
                foreach (var kvp in backendData)
                {
                    if (dataVersions.TryGetValue(kvp.Key, out long localVersion))
                    {
                        if (kvp.Value.Version > localVersion)
                        {
                            dataCache[kvp.Key] = kvp.Value.Data;
                            dataVersions[kvp.Key] = kvp.Value.Version;
                        }
                        // Else, keep local data
                    }
                    else
                    {
                        // Data exists in backend but not locally, add it
                        dataCache[kvp.Key] = kvp.Value.Data;
                        dataVersions[kvp.Key] = kvp.Value.Version;
                    }
                }
            }
        }

        private void SaveToLocalStorage()
        {
            lock (cacheLock)
            {
                string dataCacheJson = JsonConvert.SerializeObject(dataCache);
                string dataVersionsJson = JsonConvert.SerializeObject(dataVersions);

                string encryptedDataCache = EncryptData(dataCacheJson);
                string encryptedDataVersions = EncryptData(dataVersionsJson);

                localStorage.Save(DataCacheKey, encryptedDataCache);
                localStorage.Save(DataVersionsKey, encryptedDataVersions);
            }
        }

        private void LoadFromLocalStorage()
        {
            string encryptedDataCache = localStorage.Load(DataCacheKey);
            string encryptedDataVersions = localStorage.Load(DataVersionsKey);

            if (!string.IsNullOrEmpty(encryptedDataCache) && !string.IsNullOrEmpty(encryptedDataVersions))
            {
                string dataCacheJson = DecryptData(encryptedDataCache);
                string dataVersionsJson = DecryptData(encryptedDataVersions);

                var loadedDataCache = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataCacheJson);
                var loadedDataVersions = JsonConvert.DeserializeObject<Dictionary<string, long>>(dataVersionsJson);

                lock (cacheLock)
                {
                    dataCache = loadedDataCache ?? new Dictionary<string, string>();
                    dataVersions = loadedDataVersions ?? new Dictionary<string, long>();
                }
            }
        }

        private string EncryptData(string data)
        {
            return data;
        }

        private string DecryptData(string data)
        {
            return data;
        }

        [Serializable]
        private class VersionedData
        {
            public string Data;
            public long Version;
        }

        [DllImport("__Internal")]
        private static extern void SavePlayerData(
            [MarshalAs(UnmanagedType.LPUTF8Str)] string requestId,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string key,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string dataJson);

        [DllImport("__Internal")]
        private static extern void LoadPlayerData(
            [MarshalAs(UnmanagedType.LPUTF8Str)] string requestId,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string key);
    }
}