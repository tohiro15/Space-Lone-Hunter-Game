using PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.Abstractions;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.LocalStorage
{
    public class PlayerPrefsLocalStorageModule : YGModuleBase, ILocalStorageModule
    {
        public void Save(string key, string data)
        {
            PlayerPrefs.SetString(key, data);
            PlayerPrefs.Save();
        }

        public string Load(string key)
        {
            if (HasKey(key))
            {
                return PlayerPrefs.GetString(key);
            }
            else
            {
                return null;
            }
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
        }

        public override void Initialize()
        {
        }
    }
}