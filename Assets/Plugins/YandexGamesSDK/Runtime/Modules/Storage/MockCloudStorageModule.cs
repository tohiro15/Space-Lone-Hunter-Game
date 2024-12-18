using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Logging;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Storage
{
    public class MockCloudStorageModule : CloudStorageModule
    {
        private const string MockDataCacheKey = "Mock_CloudStorage_DataCache";
        private const string MockDataVersionsKey = "Mock_CloudStorage_DataVersions";

        public override void Initialize()
        {
            LoadFromLocalStorage();

            Debug.Log("Mock cloud storage initialized with local data.");
        }

        public override void Save<T>(string key, T data, Action<bool, string> callback = null)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(data);
                lock (cacheLock)
                {
                    dataCache[key] = jsonData;
                    dataVersions[key] = DateTime.UtcNow.Ticks;
                    isDirty = true;
                }

                SaveToLocalStorage();
                YGLogger.Info($"Mock save for key '{key}' with data: {jsonData}");
                callback?.Invoke(true, null);
            }
            catch (Exception ex)
            {
                YGLogger.Error($"Mock serialization error for key '{key}': {ex.Message}");
                callback?.Invoke(false, $"Serialization error: {ex.Message}");
            }
        }

        public override void Load<T>(string key, Action<bool, T, string> callback = null)
        {
            lock (cacheLock)
            {
                if (dataCache.ContainsKey(key))
                {
                    try
                    {
                        var data = JsonConvert.DeserializeObject<T>(dataCache[key]);
                        YGLogger.Info($"Mock load successful for key '{key}'.");
                        callback?.Invoke(true, data, null);
                    }
                    catch (Exception ex)
                    {
                        YGLogger.Error($"Mock deserialization error for key '{key}': {ex.Message}");
                        callback?.Invoke(false, default(T), $"Deserialization error: {ex.Message}");
                    }
                }
                else
                {
                    YGLogger.Warning($"Mock load failed: key '{key}' not found.");
                    callback?.Invoke(false, default(T), $"Key '{key}' not found.");
                }
            }
        }

        public override void FlushData(Action<bool, string> callback = null)
        {
            if (isDirty)
            {
                SaveToLocalStorage();
                YGLogger.Info("Mock data flushed successfully.");
                isDirty = false;
                callback?.Invoke(true, null);
            }
            else
            {
                YGLogger.Info("No data to flush in mock.");
                callback?.Invoke(true, "No data to flush.");
            }
        }

        private void SaveToLocalStorage()
        {
            lock (cacheLock)
            {
                string dataCacheJson = JsonConvert.SerializeObject(dataCache);
                string dataVersionsJson = JsonConvert.SerializeObject(dataVersions);

                PlayerPrefs.SetString(MockDataCacheKey, dataCacheJson);
                PlayerPrefs.SetString(MockDataVersionsKey, dataVersionsJson);
                PlayerPrefs.Save();
            }
        }

        private void LoadFromLocalStorage()
        {
            if (PlayerPrefs.HasKey(MockDataCacheKey) && PlayerPrefs.HasKey(MockDataVersionsKey))
            {
                string dataCacheJson = PlayerPrefs.GetString(MockDataCacheKey);
                string dataVersionsJson = PlayerPrefs.GetString(MockDataVersionsKey);

                dataCache = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataCacheJson) ??
                            new Dictionary<string, string>();
                dataVersions = JsonConvert.DeserializeObject<Dictionary<string, long>>(dataVersionsJson) ??
                               new Dictionary<string, long>();
            }
            else
            {
                dataCache = new Dictionary<string, string>();
                dataVersions = new Dictionary<string, long>();
                YGLogger.Info("No mock data found in local storage; starting with empty cache.");
            }
        }
    }
}