using System.Collections;
using UnityEngine;

namespace PlayablesStudio.Plugins.YandexGamesSDK.Runtime.Utilities
{
    /// <summary>
    /// Singleton class responsible for managing coroutines within the Yandex Games SDK.
    /// </summary>
    public class YGCoroutineManager : MonoBehaviour
    {
        private static YGCoroutineManager _instance;

        /// <summary>
        /// Gets the singleton instance of the YGCoroutineManager.
        /// </summary>
        public static YGCoroutineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Search for an existing instance in the scene
                    _instance = FindObjectOfType<YGCoroutineManager>();

                    if (_instance == null)
                    {
                        // Create a new GameObject if no instance exists
                        GameObject coroutineManagerObject = new GameObject("YGCoroutineManager");
                        _instance = coroutineManagerObject.AddComponent<YGCoroutineManager>();

                        // Ensure the manager persists across scene loads
                        DontDestroyOnLoad(coroutineManagerObject);
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Initializes the YGCoroutineManager. This method ensures that the manager is instantiated.
        /// </summary>
        public static void Initialize()
        {
            // Accessing the Instance property will instantiate the manager if it doesn't exist
            var _ = Instance;
        }

        /// <summary>
        /// Starts a coroutine.
        /// </summary>
        /// <param name="routine">The coroutine to start.</param>
        public void StartYGCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        /// <summary>
        /// Stops a specific coroutine.
        /// </summary>
        /// <param name="routine">The coroutine to stop.</param>
        public void StopYGCoroutine(IEnumerator routine)
        {
            StopCoroutine(routine);
        }

        /// <summary>
        /// Stops all coroutines running on the YGCoroutineManager.
        /// </summary>
        public void StopAllYGCoroutines()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// Ensures that only one instance of YGCoroutineManager exists.
        /// </summary>
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (_instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
