namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Modules.LocalStorage
{
    public interface ILocalStorageModule
    {
        /// <summary>
        /// Saves data to local storage with the specified key.
        /// </summary>
        /// <param name="key">The key under which the data is saved.</param>
        /// <param name="data">The data to save.</param>
        void Save(string key, string data);

        /// <summary>
        /// Loads data from local storage for the specified key.
        /// </summary>
        /// <param name="key">The key of the data to load.</param>
        /// <returns>The loaded data, or null if the key does not exist.</returns>
        string Load(string key);

        /// <summary>
        /// Checks if data exists in local storage for the specified key.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if data exists for the key, otherwise false.</returns>
        bool HasKey(string key);

        /// <summary>
        /// Deletes data from local storage for the specified key.
        /// </summary>
        /// <param name="key">The key of the data to delete.</param>
        void Delete(string key);
    }
}